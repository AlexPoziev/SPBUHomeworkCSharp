﻿using StackCalculator;

var temp = new StackArray();

temp.Push(5);
temp.Push(6);
temp.Push(7);

var firstTest = temp.Pop();
Console.WriteLine(firstTest);

firstTest = temp.Pop();
Console.WriteLine(firstTest);

firstTest = temp.Pop();
Console.WriteLine(firstTest);
