using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading;

namespace ScreenTime
{
    internal class ActivityMonitor : IDisposable
    {
        private IntPtr _keyboardHook;
        private IntPtr _mouseHook;

        private const Int32 WH_KEYBOARD_LL = 13;
        private const Int32 WH_MOUSE_LL = 14;

        private bool _disposed;
        private volatile bool _isActive;

        public ActivityMonitor()
        {
            _keyboardHook = WindowsHooks.SetWindowsHookEx(
                WH_KEYBOARD_LL, OnKeyboardActivity, IntPtr.Zero, 0);
            _mouseHook = WindowsHooks.SetWindowsHookEx(
                WH_MOUSE_LL, OnMouseActivity, IntPtr.Zero, 0);
        }

        public bool IsActive => _isActive;

        public void ResetActive()
        {
            _isActive = false;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                WindowsHooks.UnhookWindowsHookEx(_keyboardHook);
                WindowsHooks.UnhookWindowsHookEx(_mouseHook);

                _disposed = true;
            }
        }

        private IntPtr OnMouseActivity(
            Int32 Code, IntPtr wParam, IntPtr lParam)
        {
            if (Code < 0)
            {
                return WindowsHooks.CallNextHookEx(
                    _mouseHook, Code, wParam, lParam);
            }

            _isActive = true;

            return WindowsHooks.CallNextHookEx(
                _mouseHook, Code, wParam, lParam);
        }

        private IntPtr OnKeyboardActivity(
            Int32 Code, IntPtr wParam, IntPtr lParam)
        {
            if (Code < 0)
            {
                return WindowsHooks.CallNextHookEx(
                    _keyboardHook, Code, wParam, lParam);
            }

            _isActive = true;

            return WindowsHooks.CallNextHookEx(
                _keyboardHook, Code, wParam, lParam);
        }

        ~ActivityMonitor()
        {
            Dispose();
        }
    }
}
