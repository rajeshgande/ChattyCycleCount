   M S B A M L   `   `   `  ����K  GChattyCycleCount, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ChattyCycleCountChattyCycleCount  5   	   !    ChattyCycleCount.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:ChattyCycleCount     5   	   J FWpfAnimatedGif, Version=1.4.14.0, Culture=neutral, PublicKeyToken=null,gif"http://wpfanimatedgif.codeplex.com  5   	     � Title$  Chatty Cycle Count��5	   	   $
��1280��6$   $
��1024��62   .��5
      ]� $��3q�6   .+�5   
   �� -   5         ��Name$1 +WhatTheComputerThinksTheUserIsSayingTextbox��5      $	��Top=�5       �� 
IsReadOnly . $��16(�5      $
��Bold"�5       �� MinLines$ 1��5       �� TextWrapping$
 Wrap=�5      $
��Auto��5      $	��700��5      $��2q�5      $��Right=�5      �� 5   
   
���   �$��0q�6%   $	��700��69   $��2q�6E   $��Right=�6P   ._�5      ,� -   5         ,�Name$ ChatTranscriptViewer��5   '   $��8q�5   '   " ,� VerticalScrollBarVisibility$
 Auto=�5   '   5      �� 5   
   $
��Left=�6   .+�5      �� -   5         ��Name$ HearththrobAnimation��5      "  WpfAnimatedGif.ImageBehavior   AnimatedSource$ Images/animation.gif��62   $	��293��6j   $	��Top=�6w   $
��Left=�6�   $	��253��6�   6�   �� 5       -   5        	 ��Name$	 StartButton��5       $	��200��6*   $��50��66   $��5q�6B   $��Start Recording��6M   6g   �� 5!      -   5        $	 ButtonClearTheTranscript��5!      $	��200��67   $��50��6C   $��5q�6O   $��Clear the transcript��6Z   6y   �� 5"      -   5        $9	 3ButtonClearWhatTheComputerThinksTheComputerIsSaying��5"      $	��200��6R   $��50��6^   $��5q�6j   $!��Clear the calculation field��6u   6�   �� 5$      -   5        $	 ButtonStartAnimation��5$      $	��200��63   $��50��6?   $��5q�6K   $��Start Animation��6V   6p   �� 5%      -   5        $	 ButtonStopAnimation��5%      $	��200��62   $��50��6>   $��5q�6J   $��Stop Animation��6U   6n   5&      5(      5)      