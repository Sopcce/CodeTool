using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CodeTool.Common.Model
{
    /// <summary>
    /// �ֶ�
    /// </summary>
    public class Field : IComparable
    {
        private string _descn;

        /// <summary>
        /// ���������ݿ��
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public Table Table { get; set; }
         

        /// <summary>
        /// �Ƿ��Ǳ�ʶ
        /// </summary>
        public bool IsId { get; set; }

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        public bool IsKey { get; set; }

        /// <summary>
        /// ռ���ֽ���
        /// </summary>
        public int FieldSize { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public long FieldLength { get; set; }

        /// <summary>
        /// �Ƿ������
        /// </summary>
        public bool AllowNull { get; set; }

        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// �ֶ�˵��
        /// </summary>
        public string FieldDescn
        {
            get => !string.IsNullOrEmpty(_descn) ? _descn : FieldName;
            set => _descn = Regex.Replace(value, @"\s*[\n]+\s*", "");
        }

        /// <summary>
        /// �ֶ���������
        /// </summary>
        public string FieldType { get; set; }
      
 
        /// <summary>
        /// �ֶ���
        /// </summary>
        public string FieldName { get; internal set; }
        public int FieldNumber { get; internal set; }

       

        public int CompareTo(object obj)
        {
            Field field = obj as Field;

            if (field != null)
                return this.FieldNumber.CompareTo(field.FieldNumber);
            return FieldNumber;
        }

     
    }
}
