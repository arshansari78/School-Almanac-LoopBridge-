using System;
using System.Collections.Generic;
using System.Text;

namespace LoopBridge.ViewModels.Result
{
    public class RStudentListVm
    {
        public ApiResultVm Result { get; set; }
        public List<StudentVm> Data { get; set; }
    }
}
