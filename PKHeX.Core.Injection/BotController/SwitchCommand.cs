﻿using System.Linq;
using System.Text;

namespace PKHeX.Core.Injection
{
    /// <summary>
    /// Encodes commands for a <see cref="SysBotMini"/> to be sent as a <see cref="byte"/> array.
    /// </summary>
    public static class SwitchCommand
    {
        private static readonly Encoding Encoder = Encoding.UTF8;
        private static byte[] Encode(string command, bool addrn = true) => Encoder.GetBytes(addrn ? command + "\r\n" : command);

        /// <summary>
        /// Removes the virtual controller from the bot. Allows physical controllers to control manually.
        /// </summary>
        /// <returns>Encoded command bytes</returns>
        public static byte[] DetachController() => Encode("detachController");

        /* 
         *
         * Controller Button Commands
         *
         */

        /// <summary>
        /// Presses and releases a <see cref="SwitchButton"/> for 50ms.
        /// </summary>
        /// <param name="button">Button to click.</param>
        /// <remarks>Press &amp; Release timing is performed by the console automatically.</remarks>
        /// <returns>Encoded command bytes</returns>
        public static byte[] Click(SwitchButton button) => Encode($"click {button}");

        /// <summary>
        /// Presses and does NOT release a <see cref="SwitchButton"/>.
        /// </summary>
        /// <param name="button">Button to hold.</param>
        /// <returns>Encoded command bytes</returns>
        public static byte[] Hold(SwitchButton button) => Encode($"press {button}");

        /// <summary>
        /// Releases the held <see cref="SwitchButton"/>.
        /// </summary>
        /// <param name="button">Button to release.</param>
        /// <returns>Encoded command bytes</returns>
        public static byte[] Release(SwitchButton button) => Encode($"release {button}");

        /* 
         *
         * Controller Stick Commands
         *
         */

        /// <summary>
        /// Sets the specified <see cref="stick"/> to the desired <see cref="x"/> and <see cref="y"/> positions.
        /// </summary>
        /// <returns>Encoded command bytes</returns>
        public static byte[] SetStick(SwitchStick stick, int x, int y) => Encode($"setStick {stick} {x} {y}");

        /// <summary>
        /// Resets the specified <see cref="stick"/> to (0,0)
        /// </summary>
        /// <returns>Encoded command bytes</returns>
        public static byte[] ResetStick(SwitchStick stick) => SetStick(stick, 0, 0);

        /* 
         *
         * Memory I/O Commands
         *
         */

        /// <summary>
        /// Requests the Bot to send <see cref="count"/> bytes from <see cref="offset"/>.
        /// </summary>
        /// <param name="offset">Address of the data</param>
        /// <param name="count">Amount of bytes</param>
        /// <returns>Encoded command bytes</returns>
        public static byte[] Peek(uint offset, int count) => Encode($"peek 0x{offset:X8} {count}");

        /// <summary>
        /// Sends the Bot <see cref="data"/> to be written to <see cref="offset"/>.
        /// </summary>
        /// <param name="offset">Address of the data</param>
        /// <param name="data">Data to write</param>
        /// <returns>Encoded command bytes</returns>
        public static byte[] Poke(uint offset, byte[] data) => Encode($"poke 0x{offset:X8} 0x{string.Concat(data.Select(z => $"{z:X2}"))}");

        /// <summary>
        /// (Without return characters for USB-Botbase) Requests the Bot to send <see cref="count"/> bytes from <see cref="offset"/>.
        /// </summary>
        /// <param name="offset">Address of the data</param>
        /// <param name="count">Amount of bytes</param>
        /// <returns>Encoded command bytes</returns>
        public static byte[] PeekRaw(uint offset, int count) => Encode($"peek 0x{offset:X8} {count}", false);

        /// <summary>
        /// (Without return characters for USB-Botbase) Sends the Bot <see cref="data"/> to be written to <see cref="offset"/>.
        /// </summary>
        /// <param name="offset">Address of the data</param>
        /// <param name="data">Data to write</param>
        /// <returns>Encoded command bytes</returns>
        public static byte[] PokeRaw(uint offset, byte[] data) => Encode($"poke 0x{offset:X8} 0x{string.Concat(data.Select(z => $"{z:X2}"))}", false);

        /// <summary>
        /// Requests the Bot to send <see cref="count"/> bytes from absolute <see cref="offset"/>.
        /// </summary>
        /// <param name="offset">Absolute address of the data</param>
        /// <param name="count">Amount of bytes</param>
        /// <returns>Encoded command bytes</returns>
        public static byte[] PeekAbsolute(ulong offset, int count) => Encode($"peekAbsolute 0x{offset:X16} {count}");

        /// <summary>
        /// Sends the Bot <see cref="data"/> to be written to absolute <see cref="offset"/>.
        /// </summary>
        /// <param name="offset">Absolute address of the data</param>
        /// <param name="data">Data to write</param>
        /// <returns>Encoded command bytes</returns>
        public static byte[] PokeAbsolute(ulong offset, byte[] data) => Encode($"pokeAbsolute 0x{offset:X16} 0x{string.Concat(data.Select(z => $"{z:X2}"))}");

        /// <summary>
        /// Requests the Bot to send <see cref="count"/> bytes from main <see cref="offset"/>.
        /// </summary>
        /// <param name="offset">Address of the data relative to main</param>
        /// <param name="count">Amount of bytes</param>
        /// <returns>Encoded command bytes</returns>
        public static byte[] PeekMain(ulong offset, int count) => Encode($"peekMain 0x{offset:X16} {count}");

        /// <summary>
        /// Sends the Bot <see cref="data"/> to be written to main <see cref="offset"/>.
        /// </summary>
        /// <param name="offset">Address of the data relative to main</param>
        /// <param name="data">Data to write</param>
        /// <returns>Encoded command bytes</returns>
        public static byte[] PokeMain(ulong offset, byte[] data) => Encode($"pokeMain 0x{offset:X16} 0x{string.Concat(data.Select(z => $"{z:X2}"))}");
    }
}
