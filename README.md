# Roblox FPS Unlocker ( API )
## _Manage your Roblox FPS Unlocker in .NET ( C# )_

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

This Roblox FPS Unlocker ( Specifically ) has a Namedpipe that Customized the FPS Cap you want to set in your own User Interface using ( Trackball / Slider / Textbox / and etc )


## Features

- Control Roblox's FPS Cap ( Unlock then Manage its FPS Cap in your own UI )
- Modified Version of Roblox FPS Unlocker

## Screenshots
![enter image description here](https://i.imgur.com/UdvlzEP.png)
# Footage
https://user-images.githubusercontent.com/40538401/144061523-c0f5c90c-1889-4506-80a5-662fe0ebf24e.mp4

## How to use this API

Want to add this API to your User Interface? Great!

First, Add this API on your Project's Reference.

To Call the API. Copy this Code since this will Create an FPSUnlockAPI's object to call the following methods from

```sh
FPSUnlockAPI.FPSUnlockAPI fps = new FPSUnlockAPI.FPSUnlockAPI();
```

Using Slider ( WPF ) or Trackball ( WinForm)

```sh
            double value = trackBar1.Value; //Getting the Value of your Slider or Trackball
            fps.SendFPSValue(value); // Send the Value to the Pipe. 
```

Using Textbox / Richtextbox ( Anything but string )

```sh
            try //Exception Handler ofc ( because some ppl gonna accident putting letter A to Z or special characters that cannot be converted to double )
            {
                string valuestring = textBox1.Text;
                double value = Convert.ToDouble(valuestring); //Convert the String to Double since even you type No. on text, it will show as a string, so it gonna convert it to double. 0-9, 
                fps.SendFPSValue(value); // Send the Value to the Pipe. 
            }
            catch (Exception)
            {
                MessageBox.Show("Thats Not a Number");
                return;
            }
```

#### Credit
- Austin ( FPS Unlocker Creator / Developer ) <- https://github.com/axstin/rbxfpsunlocker
- SkieHackerYT ( .NET API and Customizing FPS Unlocker )
