﻿using System;

namespace Client.ViewModel
{
    /// <summary>
    /// Üzenet eseményargumentum típusa.
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// Üzenet lekérdezése, vagy beállítása.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Üzenet eseményargumentum példányosítása.
        /// </summary>
        /// <param name="message">Üzenet.</param>
        public MessageEventArgs(string message) { Message = message; }
    }
}