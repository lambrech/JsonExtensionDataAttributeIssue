using System;

namespace JsonExtensionDataAttributeIssue
{
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class One
    {
        public string Text { get; set; }

        public Two ReferenceTwo { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> ExtensionDataOne { get; } = new Dictionary<string, object>();
    }

    public class Two
    {
        public int Number { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> ExtensionDataTwo { get; } = new Dictionary<string, object>();
    }

    class Program
    {
        //TestCase 1
        static void Main(string[] args)
        {
            var two = new Two { Number = 2 };
            two.ExtensionDataTwo.Add("SpecialInformation", "I am class TWO");

            var one = new One { Text = "Hello World" };
            one.ExtensionDataOne.Add("SpecialInformation", "I am class ONE");
            one.ReferenceTwo = two;


            var json = JsonSerializer.Serialize(one, new JsonSerializerOptions { WriteIndented = true });

            var deserializedObject = JsonSerializer.Deserialize<One>(json);
        }


        // TestCase 2
        //static void Main(string[] args)
        //{
        //    var four = new Four { Number = 4 };
        //    four.ExtensionDataFour.Add("SpecialInformation", JsonDocument.Parse(JsonSerializer.SerializeToUtf8Bytes("I am class FOUR")).RootElement);

        //    var three = new Three { Text = "Hello World" };
        //    three.ExtensionDataThree.Add("SpecialInformation", JsonDocument.Parse(JsonSerializer.SerializeToUtf8Bytes("I am class THREE")).RootElement);
        //    three.ReferencedFours.Add(four);


        //    var json = JsonSerializer.Serialize(three, new JsonSerializerOptions { WriteIndented = true });

        //    var deserializedObject = JsonSerializer.Deserialize<Three>(json);
        //}

        public class Three
        {
            public string Text { get; set; }

            public List<Four> ReferencedFours { get; set; } = new List<Four>();

            [JsonExtensionData]
            public Dictionary<string, JsonElement> ExtensionDataThree { get; set; } = new Dictionary<string, JsonElement>();
        }

        public class Four
        {
            public int Number { get; set; }

            [JsonExtensionData]
            public Dictionary<string, JsonElement> ExtensionDataFour { get; set; } = new Dictionary<string, JsonElement>();
        }
    }
}
