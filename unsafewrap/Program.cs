using Google.Protobuf;

byte[] data = await File.ReadAllBytesAsync(@"");
ByteString copied = ByteString.CopyFrom(data);
ByteString wrapped = UnsafeByteOperations.UnsafeWrap(data);
