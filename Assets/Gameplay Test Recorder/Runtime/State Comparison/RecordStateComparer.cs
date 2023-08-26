using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    /// <summary>
    /// Compare two states for equality. Rather than a bool, the result is a float, which gets larger with bigger differences between the states.
    /// This allows a "softer" comparison and makes it so that differences in floating point numbers are less relevent that differences in bools or enums.
    /// This makes sense because floats can differ by quite a bit from one execution to another. A different value in a bool hoewever is most likely an error.
    /// </summary>
    internal class RecordStateComparer
    {
        public static readonly RecordStateComparer INSTANCE = new RecordStateComparer();

        public float Compare(IValueSpace record, IObjectState replay, ComparisonLogger logger)
        {
            Assert.IsNotNull(logger);
            if (record == null)
            {
                Debug.LogError("Failure loading recording end state! This happens sometimes due to loading errors within Unity. Try restarting Unity and rerun the replay.");
                return ReplayResultHelper.BIG_DIFFERENCE;
            }
            if (replay == null)
            {
                Debug.LogError("Cannot find replay end state!");
                return ReplayResultHelper.BIG_DIFFERENCE;
            }
            float result = 0;
            logger.Log($"Comparing ({record.RecordedType})`{record.Id}`:({record.RecordedType})`{replay.Id}`");
            logger.IndentUp();
            result += CompareIds(record, replay, logger);
            if (record is IValueSpaceCollection xh && replay is IObjectStateCollection yh)
            {
                result += CompareCollections(xh, yh, logger);
            }
            else
            {
                result += CompareRecords(record, replay.Record, logger);
            }
            logger.IndentDown();
            return result;
        }

        private float CompareCollections(IValueSpaceCollection record, IObjectStateCollection replay, ComparisonLogger logger)
        {
            float diff = ReplayResultHelper.NO_DIFFERENCE;
            foreach (IValueSpace vs in record)
            {
                if (replay.TryGetValue(vs.Id, out IObjectState otherObjectState))
                {
                    diff += Compare(vs, otherObjectState, logger);
                }
                else
                {
                    diff += ReplayResultHelper.BIG_DIFFERENCE;
                    logger.Log($"❌ No match for `{vs.Id}`; diff=`{diff}`");
                }
            }
            return diff;
        }

        private float CompareIds(IValueSpace record, IObjectState replay, ComparisonLogger logger)
        {
            if (record.Id.Equals(replay.Id))
            {
                return ReplayResultHelper.NO_DIFFERENCE;
            }
            else
            {
                logger.Log($"❌ ID mismatch `{record.Id}`!=`{replay.Id}`; diff=`{ReplayResultHelper.BIG_DIFFERENCE}`");
                return ReplayResultHelper.BIG_DIFFERENCE;
            }
        }

        private float CompareRecords(IValueSpace record, IRecord replay, ComparisonLogger logger)
        {
            Type type = replay.Get.GetType();
            if (type.IsEnum)
            {
                type = typeof(Enum);
            }
            float diff = StateComparerUtility.Compare(type, record, replay, DefaultComparisonWheights.INSTANCE);
            if (diff != 0)
            {
                logger.Log($"❌ expected=`{record}`; actual=`{replay}`; diff=`{diff}`");
            }
            return diff;
        }
    }
}