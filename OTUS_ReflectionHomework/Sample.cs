using System;

namespace OTUS_ReflectionHomework
{
    [Serializable]
    public class Sample
    {
        public int i1, i2, i3;
        public string s1, s2, s3;
        public DateTime d1;
        public static Sample Get()
        {
            return new Sample()
            {
                i1 = 1,
                i2 = 2,
                i3 = 3,
                s1 = "asdfadf asdf",
                s2 = "zxcvzvzxc ddd",
                s3 = "qwer3 3rwer",
                d1 = DateTime.Now
            };
        }
    }
}
