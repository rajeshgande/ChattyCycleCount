   M S B A M L   `   `   `  ����K  GChattyCycleCount, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ChattyCycleCountChattyCycleCount  5   	   !    ChattyCycleCount.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:ChattyCycleCount     5   	   J FWpfAnimatedGif, Version=1.4.14.0, Culture=neutral, PublicKeyToken=null,gif"http://wpfanimatedgif.codeplex.com  5   	     � Title$  Chatty Cycle Count��5   	   $
��1280��6$   $
��1024��62    � 	Resources 5         MyButtonStyle �� 
TargetType �� BasedOn �� Property �� Value��%�   &	        7���� 5	   
   " ��6&   8   .�5
      ��  � ��$ 42��6*   �� 5       � ��$
 20,0q�6'   �� 5       � ��$ Stretch=�64   5      5      .��5      � ���	#FF333333 � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5      � 
���   �$��Stretch=�6&    � RowDefinitions 5      �� 5      $
��Auto �6$   �� 5      $��* �6$   .+�5      �� -   5         ��Name$ HearththrobAnimation��5      "  WpfAnimatedGif.ImageBehavior	   AnimatedSource$	 Images/animation.gif��66   $��Uniform=�6�   A� 5      
 A� Rows$
 5��6   $��1��6'   $��Stretch=�64   .+�5      �� -   5         ��Name$ StartButton��5      $��Start Recording��62   #��[  6g   �� 5      -   5        $ 
ButtonMute��5      $
��Mute��61   #��[  6Z   �� 5      -   5        $ ButtonClearTheTranscript��5      $��Reset��6?   #��[  6j   5       5!      �� 5"   
   $��4��6   ���	#FF6666666!   � 5#   
   $��2��6   .+�5$      ]� ��. .+�5%      �� -   5         ��Name$1 +WhatTheComputerThinksTheUserIsSayingTextbox��5%      $	��Top=�5&      
���iii�$��0q�6<    �� Padding$ 8q�6G   
�������$��19(�5'      $��SemiBold"�5(      $��Arial)�6(   $
��Wrap=�5)      �� 5+      ��6   Z� ._�5,      ,� -   5         ,�Name$ ChatTranscriptViewer��5,   '   $��8q�5-   '   $��Arial)�62   " ,� VerticalScrollBarVisibility$
 Auto=�5.   '   5/      51      52      53      55      