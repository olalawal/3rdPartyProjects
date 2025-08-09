using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInstitute.Util 
{
  public class DataConversionUtility
    {
      /// <summary>
      /// Send null value for optional field in parameterized query.
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static object GetDataValue(object value)
      {
          if (value == null)
          {
              return DBNull.Value;
          }

          return value;
      }
    }
}
