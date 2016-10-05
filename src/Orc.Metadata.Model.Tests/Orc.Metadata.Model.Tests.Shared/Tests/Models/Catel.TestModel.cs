// 
// The MIT License (MIT)
// 
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the 
// Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

namespace Orc.Metadata.Model.Tests.Tests.Models
{
    using global::Catel.Data;

    public class CatelTestModel : ModelBase
    {
        #region Fields

        public static readonly PropertyData IntCatelPropertyProperty =
            RegisterProperty("IntCatelProperty", typeof(int), 0);

        public static readonly PropertyData StringCatelPropertyProperty =
            RegisterProperty("StringCatelProperty", typeof(string), null);

        #endregion



        #region Constructors

        public CatelTestModel() { }

        #endregion



        #region Properties

        public int IntCatelProperty
        {
            get { return GetValue<int>(IntCatelPropertyProperty); }
            set { SetValue(IntCatelPropertyProperty, value); }
        }

        public string StringCatelProperty
        {
            get { return GetValue<string>(StringCatelPropertyProperty); }
            set { SetValue(StringCatelPropertyProperty, value); }
        }

        public int IntProperty { get; set; }

        public string StringProperty { get; set; }

        #endregion
    }
}