using System;
using System.Collections.Generic;
using System.Text;

namespace LoopBridge.ViewModels.Result
{
    public class RDiaryListVm
    {
        public ApiResultVm Result { get; set; }
        public List<DiaryVm> Data { get; set; }
    }
}
