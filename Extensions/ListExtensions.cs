using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Merges two lists - finds equal property values from both lists
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRes"></typeparam>
        /// <typeparam name="TM"></typeparam>
        /// <typeparam name="TMRes"></typeparam>
        /// <param name="outputList">Output List</param>
        /// <param name="comparisionOutputSelector">Property name to compare two lists</param>
        /// <param name="comparisionInputSelector">Property name to compare two lists</param>
        /// <param name="mergeOutputSelector">Property name from output list</param>
        /// <param name="mergeInputSelector">Property name from input list</param>
        /// <param name="inputList">input list</param>
        /// <returns></returns>
        public static IList<T> Merge<T, TRes, TM, TMRes>(this IList<T> outputList, IList<TM> inputList, Expression<Func<T, TRes>> comparisionOutputSelector, Expression<Func<TM, TMRes>> comparisionInputSelector, Expression<Func<T, TRes>> mergeOutputSelector, Expression<Func<TM, TMRes>> mergeInputSelector)
        {
            var outputMember = comparisionOutputSelector.Body as MemberExpression;
            var inputMember = comparisionInputSelector.Body as MemberExpression;

            var outputSelectorMember = mergeOutputSelector.Body as MemberExpression;
            var inputSelectorMember = mergeInputSelector.Body as MemberExpression;

            var outputPropertyComparisionName = outputMember.Member.Name; ;
            var inputPropertyComparisionName = inputMember.Member.Name;

            var outputPropertySelectorName = outputSelectorMember.Member.Name; ;
            var inputPropertySelectorName = inputSelectorMember.Member.Name;

            var newOutputList = outputList.ToList();
            var newInputList = inputList.ToList();

            foreach (var outputItem in newOutputList)
            {
                var outputValue = outputItem.GetPropValue(outputPropertyComparisionName);
                foreach (var inputItem in newInputList)
                {
                    var inputValue = inputItem.GetPropValue(inputPropertyComparisionName);
                    if (outputValue.Equals(inputValue))
                    {
                        var setValue = inputItem.GetPropValue(inputPropertySelectorName);
                        outputItem.SetPropValue(outputPropertySelectorName, setValue.ToString());
                    }
                }
            }

            return newOutputList;
        }
    }
}
