using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    /// <summary>
    /// Базовая модель теста
    /// </summary>
    public class ModelBase
    {
        /// <summary>
        /// ID автотеста
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Id связанного ручного теста
        /// </summary>
        public int? ManualTestId { get; set; }
        public override string ToString() => ManualTestId?.ToString();
    }
}
