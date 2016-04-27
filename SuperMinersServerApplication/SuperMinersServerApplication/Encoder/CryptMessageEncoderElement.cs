using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Encoder
{
    public class CryptMessageEncoderElement : MessageEncodingBindingElement
    {
        private MessageEncodingBindingElement _inner;

        public CryptMessageEncoderElement(MessageEncodingBindingElement ele)
        {
            this._inner = ele;
        }

        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            context.BindingParameters.Add(this);
            return context.BuildInnerChannelFactory<TChannel>();
        }

        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            context.BindingParameters.Add(this);
            return context.BuildInnerChannelListener<TChannel>();
        }

        public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
        {
            context.BindingParameters.Add(this);
            return context.CanBuildInnerChannelFactory<TChannel>();
        }

        public override bool CanBuildChannelListener<TChannel>(BindingContext context)
        {
            context.BindingParameters.Add(this);
            return context.CanBuildInnerChannelListener<TChannel>();
        }

        public override T GetProperty<T>(BindingContext context)
        {
            return this._inner.GetProperty<T>(context);
        }

        public override MessageEncoderFactory CreateMessageEncoderFactory()
        {
            return new CryptEncoderFactory(this._inner);
        }

        public override MessageVersion MessageVersion
        {
            get
            {
                return this._inner.MessageVersion;
            }
            set
            {
                this._inner.MessageVersion = value;
            }
        }

        public override BindingElement Clone()
        {
            return new CryptMessageEncoderElement(this._inner);
        }
    }

    public class CryptEncoderFactory : MessageEncoderFactory
    {
        private CryptEncoder _encoder;
        private MessageEncodingBindingElement _inner;

        public CryptEncoderFactory(MessageEncodingBindingElement ele)
        {
            this._inner = ele;
            this._encoder = new CryptEncoder(ele);
        }

        public override MessageEncoder Encoder
        {
            get
            {
                return _encoder;
            }
        }

        public override MessageVersion MessageVersion
        {
            get
            {
                return this._inner.MessageVersion;
            }
        }
    }
}
