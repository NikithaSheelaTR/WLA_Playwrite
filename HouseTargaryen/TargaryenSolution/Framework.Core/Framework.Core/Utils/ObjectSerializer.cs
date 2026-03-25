namespace Framework.Core.Utils
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    /// <summary>
    /// Enables serialization and de-serialization of objects.
    /// </summary>
    public static class ObjectSerializer
    {
        /// <summary>
        /// Deserialises an object from a stream by means of the specified serialiser.
        /// </summary>
        /// <param name="serializedObjectStream">The stream that contains a serialised object.
        /// </param>
        /// <typeparam name="TSerializer">The type of the serialiser to use.</typeparam>
        /// <typeparam name="TObject">The type of the object to deserialise.</typeparam>
        /// <returns>The desired object.</returns>
        public static TObject DeserializeObject<TSerializer, TObject>(Stream serializedObjectStream)
            where TSerializer : XmlObjectSerializer
        {
            return
                ObjectSerializer.DeserializeObject<TSerializer, TObject>(
                    (TSerializer)Activator.CreateInstance(typeof(TSerializer), typeof(TObject)),
                    serializedObjectStream);
        }

        /// <summary>
        /// Deserialises an object from a stream by means of the specified serialiser.
        /// </summary>
        /// <param name="serializer">
        /// The serializer with required settings.
        /// </param>
        /// <param name="serializedObjectStream">
        /// The stream that contains a serialised object.
        /// </param>
        /// <typeparam name="TSerializer">
        /// The type of the serialiser to use.
        /// </typeparam>
        /// <typeparam name="TObject">
        /// The type of the object to deserialise.
        /// </typeparam>
        /// <returns>
        /// The desired object
        /// </returns>
        public static TObject DeserializeObject<TSerializer, TObject>(
            this TSerializer serializer,
            Stream serializedObjectStream) where TSerializer : XmlObjectSerializer
        {
            return ObjectSerializer.DeserializeObjectInternal<TSerializer, TObject>(
                serializedObjectStream,
                (s, stream) => s.ReadObject(serializedObjectStream),
                serializer);
        }

        /// <summary>
        /// Deserialises an object from a string by means of the specified serializer.
        /// </summary>
        /// <param name="serializedObject">The string that contains a serialised object.
        /// </param>
        /// <typeparam name="TSerializer">The type of the serialiser to use.</typeparam>
        /// <typeparam name="TObject">The type of the object to deserialise.</typeparam>
        /// <returns>The desired object.</returns>
        public static TObject DeserializeObject<TSerializer, TObject>(string serializedObject)
            where TSerializer : XmlObjectSerializer
        {
            return
                ObjectSerializer.DeserializeObject<TSerializer, TObject>(
                    (TSerializer)Activator.CreateInstance(typeof(TSerializer), typeof(TObject)),
                    serializedObject);
        }

        /// <summary>
        /// Deserialises an object from a string by means of the JsonConvert.
        /// </summary>
        /// <param name="serializedObject">The string that contains a serialised object.
        /// </param>
        /// <typeparam name="TSerializer">The type of the serialiser to use.</typeparam>
        /// <typeparam name="TObject">The type of the object to deserialise.</typeparam>
        /// <returns>The desired object.</returns>
        public static TObject DeserializeJsonObject<TSerializer, TObject>(string serializedObject)
          where TSerializer : JsonConverter
        {
            return
                JsonConvert.DeserializeObject<TObject>(
                    serializedObject,
                    (TSerializer)Activator.CreateInstance(typeof(TSerializer)));
        }

        /// <summary>
        /// Deserialises an object from a string by means of the specified serializer.
        /// </summary>
        /// <param name="serializer">
        /// The serializer with required settings.
        /// </param>
        /// <param name="serializedObject">
        /// The string that contains a serialised object.
        /// </param>
        /// <typeparam name="TSerializer">
        /// The type of the serialiser to use.
        /// </typeparam>
        /// <typeparam name="TObject">
        /// The type of the object to deserialise.
        /// </typeparam>
        /// <returns>
        /// The desired object.
        /// </returns>
        public static TObject DeserializeObject<TSerializer, TObject>(
            this TSerializer serializer,
            string serializedObject) where TSerializer : XmlObjectSerializer
        {
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(serializedObject));

            return ObjectSerializer.DeserializeObject<TSerializer, TObject>(serializer, memoryStream);
        }

        /// <summary>
        /// Deserialises an object from a string by means of the XmlSerializer serializer.
        /// </summary>
        /// <param name="serializer">
        /// The serializer with required settings.
        /// </param>
        /// <param name="serializedObject">
        /// The string that contains a serialised object.
        /// </param>
        /// <typeparam name="TObject">
        /// The type of the object to deserialise.
        /// </typeparam>
        /// <returns>
        /// The desired object.
        /// </returns>
        public static TObject DeserializeObject<TObject>(this XmlObjectSerializer serializer, string serializedObject)
        {
            return ObjectSerializer.DeserializeObject<XmlObjectSerializer, TObject>(serializer, serializedObject);
        }

        /// <summary>
        /// Deserializes an object from a stream by means of XmlSerializer serialiser.
        /// </summary>
        /// <param name="serializedObjectStream">The stream that contains a serialized object.
        /// </param>
        /// <typeparam name="TObject">The type of the object to deserialize.</typeparam>
        /// <returns>The desired object.</returns>
        public static TObject DeserializeObject<TObject>(Stream serializedObjectStream)
        {
            return ObjectSerializer.DeserializeObjectInternal<XmlSerializer, TObject>(
                serializedObjectStream,
                (s, stream) => s.Deserialize(serializedObjectStream),
                (XmlSerializer)Activator.CreateInstance(typeof(XmlSerializer), typeof(TObject)));
        }

        /// <summary>
        /// Deserializes an object from a string by means of XmlSerializer serialiser.
        /// </summary>
        /// <param name="serializedObject">The string that contains a serialized object.
        /// </param>
        /// <typeparam name="TObject">The type of the object to deserialize.</typeparam>
        /// <returns>The desired object.</returns>
        public static TObject DeserializeObject<TObject>(string serializedObject)
        {
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(serializedObject));

            return ObjectSerializer.DeserializeObject<TObject>(memoryStream);
        }

        /// <summary>
        /// The deserialize json to object.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="jsonBody">The json body.</param>
        /// <returns></returns>
        public static T DeserializeJsonToObject<T>(string jsonBody)
            => JsonConvert.DeserializeObject<T>(jsonBody);

        /// <summary>
        /// Serializes an object to a string by means of the specified serialiser.
        /// </summary>
        /// <param name="objectToSerialize">The object to serialize.</param>
        /// <typeparam name="TSerializer">The type of the serialiser to use.</typeparam>
        /// <typeparam name="TObject">The type of the object to serialise.</typeparam>
        /// <returns>The <see cref="string"/> representation of the object.</returns>
        public static string SerializeObject<TSerializer, TObject>(TObject objectToSerialize)
            where TSerializer : XmlObjectSerializer
        {
            return ObjectSerializer.SerializeObjectInternal<TSerializer, TObject>(
                objectToSerialize,
                (s, stream, obj) => s.WriteObject(stream, obj));
        }
        
        /// <summary>
        /// Serializes an object to a string by means of the XmlSerializer serialiser.
        /// </summary>
        /// <param name="objectToSerialize">The object to serialize.</param>
        /// <typeparam name="TObject">The type of the object to serialise.</typeparam>
        /// <returns>The <see cref="string"/> representation of the object.</returns>
        public static string SerializeObject<TObject>(TObject objectToSerialize)
        {
            return ObjectSerializer.SerializeObjectInternal<XmlSerializer, TObject>(
                objectToSerialize,
                (s, stream, obj) => s.Serialize(stream, obj));
        }

        /// <summary>
        /// Serialises an object to json format.
        /// </summary>
        /// <param name="objectToSerialize">The object to be serialized to json.
        /// </param>
        /// <typeparam name="TObject">The type of the object to deserialise.</typeparam>
        /// <returns>String in json format</returns>
        public static string SerializeJsonObject<TObject>(TObject objectToSerialize)
            where TObject : class
            => JsonConvert.SerializeObject(objectToSerialize);
        
        private static TObject DeserializeObjectInternal<TSerializer, TObject>(
            Stream serializedObjectStream,
            Func<TSerializer, Stream, object> deserialisationRoutine,
            TSerializer serializer)
        {
            if (serializer == null)
            {
                throw new ArgumentException("Invalid name", nameof(serializer));
            }

            TObject result;
            using (serializedObjectStream)
            {
                result = (TObject)deserialisationRoutine(serializer, serializedObjectStream);
            }

            return result;
        }

        private static string SerializeObjectInternal<TSerializer, TObject>(
            TObject objectToSerialize,
            Action<TSerializer, Stream, object> serialisationRoutine)
        {
            string result;

            using (var localStream = new MemoryStream())
            {
                var serializer = (TSerializer)Activator.CreateInstance(typeof(TSerializer), typeof(TObject));
                serialisationRoutine(serializer, localStream, objectToSerialize);
                localStream.Position = 0;

                using (var sr = new StreamReader(localStream))
                {
                    result = sr.ReadToEnd();
                }
            }

            return result;
        }
    }
}