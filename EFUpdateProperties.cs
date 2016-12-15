using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace AlexYoung.EFCoreEntityUpdater
{
    public static class EFUpdateProperties
    {
        public static TOrig UpdateProperties<TOrig, TDTO>(this TOrig original, TDTO dto, bool includeId = false)
        {
            // Get properties of the original object (the one we wish to update)
            var origProps = typeof(TOrig).GetProperties();
            
            // Get properties of the new object (with updated property values)
            var dtoProps = typeof(TDTO).GetProperties();

            // Include every property...
            Func<PropertyInfo, bool> idExcluder = origProp => true;

            // Unless includeId is set to true, exclude any property that begins with "id"
            if (!includeId)
                idExcluder = origProp => origProp.Name.Substring(origProp.Name.Length - 2).ToLower() != "id";

            // Loop through all properties in new object
            foreach (PropertyInfo dtoProp in dtoProps)
            {
                // find the matching property on the original
                // unless it is excluded because it begins with "id"
                // if it exist, call its SetMethod on the original object
                // with the new object's value
                
                origProps
                    .Where(origProp => origProp.Name == dtoProp.Name)
                    .Where(idExcluder)
                    .SingleOrDefault()
                    ?.SetMethod.Invoke(
                        original, 
                        new Object[] 
                            {
                                dtoProp.GetMethod.Invoke(dto, null)
                            });
            }

            return original;
        }
    }
}
