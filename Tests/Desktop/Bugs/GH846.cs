// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Dicom.Log;
using Dicom.Network;
using Xunit;

namespace Dicom.Bugs
{
    public class GH846
    {
        [Fact]
        public void DicomDatasetDumper_ShallNotThrow_OnLongPrivateTags()
        {
            var dataSet = new DicomDataset(new List<DicomItem>
            {
                new DicomUniqueIdentifier(
                    new DicomTag(0x0029, 0x1009, "A VERY LONG PRIVATE CREATOR THAT SHOULD WORK BUT IT DOESN'T"),
                    "123456789.123456789.123456789")
            });

            var stringBuilder = new StringBuilder();
            var dumper = new DicomDatasetDumper(stringBuilder, 128, 64);
            var walker = new DicomDatasetWalker(dataSet);

            walker.Walk(dumper);

            var expected = "(0029,1009:A VERY LONG PRIVATE CREATOR THAT SHOULD WORK BUT IT DOESN'T) UI [123456789.123456789.123456789]#    30, Unknown\r\n";
            var actual = stringBuilder.ToString();

            Assert.Equal(expected, actual);
        }
    }
}
