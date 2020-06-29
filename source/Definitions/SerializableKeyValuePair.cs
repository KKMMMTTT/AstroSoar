using System;

namespace Game.Definitions
{
    [Serializable]
    public class SerializableKeyValuePair
    {
        public dynamic Key { get; set; }
        public dynamic Value { get; set; }

        public SerializableKeyValuePair() {

        }

        public SerializableKeyValuePair(dynamic key, dynamic value) {
            this.Key = key;
            this.Value = value;
        }
    }
}
