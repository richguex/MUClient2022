﻿// <copyright file="Decryptor.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace MUnique.OpenMU.Network
{
    using MUnique.OpenMU.Network.SimpleModulus;
    using MUnique.OpenMU.Network.Xor;

    /// <summary>
    /// The default decryptor used by the server to decrypt incoming data packets.
    /// It decrypts with the "simple modulus" algorithm first, and then with the 32 byte XOR-key.
    /// </summary>
    public class Decryptor : ComposableDecryptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Decryptor"/> class.
        /// </summary>
        /// <param name="decryptionKey">The decryption key.</param>
        /// <param name="xor32Key">The xor32 key.</param>
        public Decryptor(SimpleModulusKeys decryptionKey)
        {
            this.AddDecryptor(new SimpleModulusDecryptor(decryptionKey) { AcceptWrongBlockChecksum = true });
        }
    }

    /// <summary>
    /// The default decryptor used by the server to decrypt incoming data packets.
    /// It decrypts with the "simple modulus" algorithm first, and then with the 32 byte XOR-key.
    /// </summary>
    public class ServerDecryptor : ComposableDecryptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Decryptor"/> class.
        /// </summary>
        /// <param name="decryptionKey">The decryption key.</param>
        /// <param name="xor32Key">The xor32 key.</param>
        public ServerDecryptor(SimpleModulusKeys decryptionKey)
        {
            this.AddDecryptor(new SimpleModulusDecryptor(decryptionKey) { AcceptWrongBlockChecksum = true })
                .AddDecryptor(new Xor32Decryptor());
        }
    }
}
