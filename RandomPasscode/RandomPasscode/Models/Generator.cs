#pragma warning disable CS8618

using System;

using System.ComponentModel.DataAnnotations;

namespace RandomPasscode.Models;

public class Generator
{
    public string passcode = RandPasscode();
    public static string RandPasscode()
    {
        string passcode = "";
        Random rand = new Random();
        string passcodechars = "1234567890!@#$%^&*qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
        for (int i = 0; i < 14; i++)
        {
            int num = rand.Next(0, passcodechars.Length);
            char passcodechar = passcodechars[num];
            passcode += passcodechar;
        }
        return passcode;
    }
}
