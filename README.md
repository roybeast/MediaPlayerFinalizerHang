### MediaPlayer Finalizer Hang

This demos a finalizer hang that results in Windows `MediaPlayer` being unable to open/play/close any future videos until the whole application is terminated.

Windows Media Player (app) suffers this same issue when attempting to play the `badVideo.mp4`.


### How to build (Windows) and reproduce the issue

1. Install latest visual studio from https://visualstudio.microsoft.com/downloads
2. Be sure to include workload to build WPF apps
3. Open up `MediaPlayerFinalizerHang.sln` to open solution in visual studio
4. Build and debug the app by pressing F5
5. Good video (5 seconds long) will start looping
6. Try to play bad video (it won't and now the player is frozen)
7. Try to play good video (still frozen)
8. Try to close the window (one message box will show, the other will not)
9. Pause the debugger and see we're stuck [here](https://github.com/roybeast/MediaPlayerFinalizerHang/blob/6e6e0829cb3a70f73a69fb7ad0a24881004acad1/MediaPlayerFinalizerHang/MainWindow.xaml.cs#L27)

If you open this app and only let the good video play, then you can gracefully close the window and see the two message boxes.
