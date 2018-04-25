using System;
using System.Collections.Generic;
using Gvitech.CityMaker.FdeCore;
using ICSharpCode.Core;
using DF3DEdit.Interface;

namespace DF3DEdit.Class
{
    public abstract class AbstractBatcheEdit : IBatcheEdit
    {
        public abstract bool BeginEdit(bool bNeedTooltip);
        public abstract bool BeginEdit();
        public abstract void DoWork(EditType editType, EditParameters parameters);
        public abstract void EndEdit();
        public string UpdateRowBuffer(ref IRowBuffer row, string fieldName, System.Collections.Generic.IList<RegexDataStruct> regexDataList)
        {
            string result = string.Empty;
            try
            {
                int num = row.FieldIndex(fieldName);
                if (num != -1)
                {
                    IFieldInfo fieldInfo = row.Fields.Get(num);
                    if (regexDataList == null)
                    {
                        if (fieldInfo.Nullable)
                        {
                            row.SetNull(num);
                        }
                    }
                    else
                    {
                        switch (fieldInfo.FieldType)
                        {
                            case gviFieldType.gviFieldInt16:
                            case gviFieldType.gviFieldInt32:
                            case gviFieldType.gviFieldInt64:
                            case gviFieldType.gviFieldFloat:
                            case gviFieldType.gviFieldDouble:
                                {
                                    double num2 = 0.0;
                                    string text = "";
                                    foreach (RegexDataStruct current in regexDataList)
                                    {
                                        double num3 = 0.0;
                                        if (current.CharType == CharactorType.ConstKey)
                                        {
                                            bool flag = double.TryParse(current.Key, out num3);
                                            if (flag)
                                            {
                                                string a;
                                                if ((a = text) != null)
                                                {
                                                    if (!(a == "+"))
                                                    {
                                                        if (!(a == "-"))
                                                        {
                                                            if (!(a == "*"))
                                                            {
                                                                if (a == "/")
                                                                {
                                                                    if (flag && num3.CompareTo(0.0) != 0)
                                                                    {
                                                                        num2 /= num3;
                                                                        continue;
                                                                    }
                                                                    continue;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (flag)
                                                                {
                                                                    num2 *= num3;
                                                                    continue;
                                                                }
                                                                continue;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (flag)
                                                            {
                                                                num2 -= num3;
                                                                continue;
                                                            }
                                                            continue;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (flag)
                                                        {
                                                            num2 += num3;
                                                            continue;
                                                        }
                                                        continue;
                                                    }
                                                }
                                                num2 = num3;
                                            }
                                        }
                                        else
                                        {
                                            if (current.CharType == CharactorType.FieldKey)
                                            {
                                                int num4 = row.FieldIndex(current.Key);
                                                if (num4 != -1 && !row.IsNull(num4))
                                                {
                                                    bool flag = double.TryParse(row.GetValue(num4).ToString(), out num3);
                                                    if (flag)
                                                    {
                                                        string a2;
                                                        if ((a2 = text) != null)
                                                        {
                                                            if (!(a2 == "+"))
                                                            {
                                                                if (!(a2 == "-"))
                                                                {
                                                                    if (!(a2 == "*"))
                                                                    {
                                                                        if (a2 == "/")
                                                                        {
                                                                            if (flag && num3.CompareTo(0.0) != 0)
                                                                            {
                                                                                num2 /= num3;
                                                                                continue;
                                                                            }
                                                                            continue;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (flag)
                                                                        {
                                                                            num2 *= num3;
                                                                            continue;
                                                                        }
                                                                        continue;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (flag)
                                                                    {
                                                                        num2 -= num3;
                                                                        continue;
                                                                    }
                                                                    continue;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (flag)
                                                                {
                                                                    num2 += num3;
                                                                    continue;
                                                                }
                                                                continue;
                                                            }
                                                        }
                                                        num2 = num3;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (current.CharType == CharactorType.OptionKey)
                                                {
                                                    text = current.Key;
                                                }
                                            }
                                        }
                                    }
                                    row.SetValue(num, num2);
                                    result = num2.ToString();
                                    break;
                                }
                            case gviFieldType.gviFieldString:
                                {
                                    RegexDataStruct regexDataStruct = regexDataList[0];
                                    if (regexDataStruct.CharType == CharactorType.ConstKey)
                                    {
                                        result = regexDataStruct.Key;
                                        if (!string.IsNullOrEmpty(regexDataStruct.Key))
                                        {
                                            row.SetValue(num, regexDataStruct.Key);
                                        }
                                        else
                                        {
                                            row.SetNull(num);
                                        }
                                    }
                                    else
                                    {
                                        if (regexDataStruct.CharType == CharactorType.FieldKey)
                                        {
                                            int num5 = row.FieldIndex(regexDataStruct.Key);
                                            if (num5 != -1)
                                            {
                                                if (row.IsNull(num5))
                                                {
                                                    row.SetNull(num);
                                                }
                                                else
                                                {
                                                    object value = row.GetValue(num5);
                                                    row.SetValue(num, value);
                                                    result = value.ToString();
                                                }
                                            }
                                        }
                                    }
                                    break;
                                }
                            case gviFieldType.gviFieldDate:
                                {
                                    RegexDataStruct regexDataStruct2 = regexDataList[0];
                                    if (regexDataStruct2.CharType == CharactorType.ConstKey)
                                    {
                                        if (string.IsNullOrEmpty(regexDataStruct2.Key))
                                        {
                                            row.SetNull(num);
                                        }
                                        else
                                        {
                                            System.DateTime dateTime;
                                            bool flag2 = System.DateTime.TryParse(regexDataStruct2.Key, out dateTime);
                                            if (flag2)
                                            {
                                                row.SetValue(num, dateTime);
                                                result = dateTime.ToString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (regexDataStruct2.CharType == CharactorType.FieldKey)
                                        {
                                            int num6 = row.FieldIndex(regexDataStruct2.Key);
                                            if (num6 != -1)
                                            {
                                                if (row.IsNull(num6))
                                                {
                                                    row.SetNull(num);
                                                }
                                                else
                                                {
                                                    object value2 = row.GetValue(num6);
                                                    row.SetValue(num, value2);
                                                    result = value2.ToString();
                                                }
                                            }
                                        }
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                LoggingService.Error(e.Message);
            }
            return result;
        }
    }
}
