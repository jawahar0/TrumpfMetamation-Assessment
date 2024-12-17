using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using InputSimulatorStandard;
using InputSimulatorStandard.Native;

class Program
{
    static void Main()
    {
        // Step 1: Open File Explorer
        OpenFileExplorer();

        // Step 2: Navigate to C:\
        NavigateToCDrive();

        // Step 3: Create a folder named TrumpfMetamation
        CreateFolder();

        // Step 4: Create the Welcome.txt file inside the folder
        string filePath = @"C:\TrumpfMetamation\Welcome.txt";
        CreateFile(filePath);

        // Step 5: Write "Welcome to Trumpf Metamation!" inside the Welcome.txt file
        WriteToFile(filePath);

        // Step 6: Verify the file content
        VerifyFileContent(filePath);

        // Step 7: Delete the file and the folder
        DeleteFileAndFolder(filePath);

        // Step 8: Confirm the file and folder have been deleted
        ConfirmDeletion();
    }

    // Step 1: Open File Explorer
    static void OpenFileExplorer()
    {
        Process.Start("explorer.exe");
        Thread.Sleep(1000); // Wait for the File Explorer to open
    }

    // Step 2: Navigate to C:\
    static void NavigateToCDrive()
    {
        var sim = new InputSimulator();
        sim.Keyboard.TextEntry("C:\\");
        sim.Keyboard.KeyPress(VirtualKeyCode.RETURN); // Press Enter
        Thread.Sleep(1000); // Wait for the C: drive to open
    }

    // Step 3: Create a folder named TrumpfMetamation
    static void CreateFolder()
    {
        var sim = new InputSimulator();
        sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.SHIFT, VirtualKeyCode.VK_N); // Ctrl+Shift+N to create new folder
        Thread.Sleep(500); // Wait for the new folder to appear

        sim.Keyboard.TextEntry("TrumpfMetamation");
        sim.Keyboard.KeyPress(VirtualKeyCode.RETURN); // Press Enter to rename the folder
        Thread.Sleep(500); // Wait for the folder to be renamed
    }

    // Step 4: Create the Welcome.txt file inside the folder
    static void CreateFile(string filePath)
    {
        // Use Windows Explorer to open context menu and create a new text file
        var sim = new InputSimulator();
        sim.Mouse.MoveMouseTo(200, 200); // Approximate position for the context menu
        sim.Mouse.RightButtonClick();
        Thread.Sleep(500);

        sim.Keyboard.TextEntry("New");
        sim.Keyboard.KeyPress(VirtualKeyCode.RETURN); // Open "New" context menu
        Thread.Sleep(500);

        sim.Keyboard.TextEntry("Text Document");
        sim.Keyboard.KeyPress(VirtualKeyCode.RETURN); // Select "Text Document"
        Thread.Sleep(1000);

        sim.Keyboard.TextEntry("Welcome.txt");
        sim.Keyboard.KeyPress(VirtualKeyCode.RETURN); // Rename the file
        Thread.Sleep(500);
    }

    // Step 5: Write content to Welcome.txt
    static void WriteToFile(string filePath)
    {
        // Open the file for writing
        File.WriteAllText(filePath, "Welcome to Trumpf Metamation!");
        Thread.Sleep(500); // Wait for the content to be written
    }

    // Step 6: Verify the file content
    static void VerifyFileContent(string filePath)
    {
        string content = File.ReadAllText(filePath);
        if (content == "Welcome to Trumpf Metamation!")
        {
            Console.WriteLine("File content is correct.");
        }
        else
        {
            Console.WriteLine("File content is incorrect.");
        }
    }

    // Step 7: Delete the file and folder
    static void DeleteFileAndFolder(string filePath)
    {
        File.Delete(filePath); // Delete the file
        Thread.Sleep(500); // Wait for deletion

        // Delete the folder
        Directory.Delete(Path.GetDirectoryName(filePath));
        Thread.Sleep(500); // Wait for folder deletion
    }

    // Step 8: Confirm deletion
    static void ConfirmDeletion()
    {
        string folderPath = @"C:\TrumpfMetamation";
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine("Folder and file have been deleted successfully.");
        }
        else
        {
            Console.WriteLine("Failed to delete the folder or file.");
        }
    }
}
