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
        /// �ֶ�����
        /// </summary>
        public int Pos { get; set; }

        /// <summary>
        /// �ֶ���
        /// </summary>
        public string Name { get; set; }

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
        public int Size { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public long Length { get; set; }

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
        public string Descn
        {
            get => !string.IsNullOrEmpty(_descn) ? _descn : Name;
            set => _descn = Regex.Replace(value, @"\s*[\n]+\s*", "");
        }

        /// <summary>
        /// �ֶ���������
        /// </summary>
        public string FieldType { get; set; }

        #region IComparable ��Ա

        public int CompareTo(object obj)
        {
            Field field = obj as Field;

            if (field != null)
                return this.Pos.CompareTo(field.Pos);
            return Pos;
        }

        #endregion
    }
}
