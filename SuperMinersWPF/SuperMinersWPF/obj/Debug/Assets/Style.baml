   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   MainPanelBackground  �� Color  LogoPanelBackground  NotifyPanelBackground  PlayerInfoBackground " DataGridColumnHeaderBackground  SubWindowStyle �� 
TargetType �� Property �� Value  	Color_024  RedButtonBackground  SliderCheckBox �� 
TargetType �� Orientation'   !System.Windows.VisualStateManager    VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard	 g� Duration	
 W� To �� TargetProperty �� 
TargetName ��Name W� From �� ElementName �� Path	 �� To �� From ��Name �� CornerRadius J� RenderTransformOrigin J� Opacity ��Name 	 UserInfoButtonStyle�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %  &	        &	 #     &	 F     &	 i     &	 �     &	 �     &	 �    &	 �    &	 �    7  &		 �
    7  7  �� 5      $  	#FFF0F0F0��62   �� 5      $  	#FF4CADA6��62   �� 5      $  	#FF90E5DF��64   �� 5      $  	#FF313443��63   �� 5      $  	#FFAFBCBB��6=   �� 5
      " �6#   .�5   
   ��  � ��$	 500��6#   �� 5   
    � ��$	 600��6"   �� 5   
    �@�
ResizeMode$ NoResize=�6'   �� 5   
    �@�Topmost . 6$   �� 5   
    �@�ShowInTaskbar .  6*   5      ��5      	#FF00AADE��6)   �� 5      $  	#FFF69068��62   �� 5      " ��6#   .�5   
   ��  � ��$ 1q�6,   �� 5   
    �@�Cursor$
 Hand��6#   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      �� 
������ $ 
Horizontal=�6:    5        5      CommonStates��. 5   "     
Normal��6.     5   "   	MouseOver��6.     5    "   Pressed��6.     5!   "   Disabled��. 5"   &   �� .��5#   *   W� $	 0D�6:   $	
 .55X�6G   $ (UIElement.Opacity)�6P   $ contentPresenter��6�   W� 5$   *   $	 0D�6:   $
 1X�6G   $ (UIElement.Opacity)�6N   $ disabled��6~   5%   '   5&   #   5'        5(      CheckStates��. 5)   "     Checked��. 5*   &   �� $ Storyboard1��61   .��5+   *   W� $ 	CheckFlag��6:   $8 2(UIElement.RenderTransform).(TranslateTransform.X)�5,      $
 14X�5.      $	 
00:00:00.1D�5/       5-      �� $ 
baseBorder��$ RenderTransform.X�5/      �� 50   *   $ 
baseBorder��69   $1 +(Border.Background).(SolidColorBrush.Color)�6\   8   $	 
00:00:00.1D�53       51      �� $ 
baseBorder��$ 
Background�53      54   '   55   #     56   "   	Unchecked��. 57   &   �� $ Storyboard2��61   .��58   *   W� $ 	CheckFlag��6:   $8 2(UIElement.RenderTransform).(TranslateTransform.X)�59      $
 0X�5;      $	 
00:00:00.1D�5<       5:      �� $ 
baseBorder��$ RenderTransform.X�5<      �� 5=   *   $ 
baseBorder��69   $1 +(Border.Background).(SolidColorBrush.Color)�6\   $
 GrayX�5?      $	 
00:00:00.1D�5@       5>      �� $ 
baseBorder��$ 
Background�5@      5A   '   5B   #   5C      .+�5E      � .+�5F      �� $ 
baseBorder��6%   $��1q�69   
����ح�$��35��6d   $��20��6o   $ 10��6{   ._�5G   "   �� .+�5H   &   �� $ 	CheckFlag��6@   
�������$ 10��6S   $��Center=�6e   $��1q�6�   $��19��6�   $��18��6�   $ 0.5,0.5&�6�   z�5I   *   T� 5J   .   5L   '   5M   #   5N      �� 5O      $ disabled��6%   $��1q�67   ���	#A0C3C3C3$��35��6b   $��20��6m   $ 0Q�6y   $ 10��6�   5P      �� 5Q      $ contentPresenter��6+   #��z��6E   #��z��6y   #��z��6�   #��z��6�   #��z��6  5R      5S      5U      5V      �� 5X      " ��6(   .�5Y   
   ��  � ��$ 75��6"   �� 5Z   
    � ��$ 5q�6#   �� 5[   
    � ��$ 1q�6,   �� 5\   
    � ��8   6(   �� 5]   
    � ��
 ���� 6'   �� 5^   
    � ��8  6'   5_      5`      