﻿namespace TALXIS.TestKit.Bindings.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using TALXIS.TestKit.Selectors.Browser;
    using YamlDotNet.Core;
    using YamlDotNet.Core.Events;
    using YamlDotNet.Serialization;

    /// <summary>
    /// <see cref="IYamlTypeConverter"/> for <see cref="BrowserCredentials"/>.
    /// </summary>
    public sealed class BrowserCredentialsYamlTypeConverter : IYamlTypeConverter
    {
        /// <inheritdoc/>
        public bool Accepts(Type type)
        {
            return type == typeof(BrowserCredentials);
        }

        /// <inheritdoc/>
        public object ReadYaml(IParser parser, Type type)
        {
            var dictionary = new Dictionary<string, string>();

            parser.Consume<MappingStart>();
            while (!parser.TryConsume<MappingEnd>(out _))
            {
                dictionary.Add(parser.Consume<Scalar>().Value.ToLower(CultureInfo.CurrentCulture), parser.Consume<Scalar>().Value);
            }

            return new BrowserCredentials(dictionary["username"], dictionary["password"]);
        }

        /// <inheritdoc/>
        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            throw new NotImplementedException();
        }
    }
}
