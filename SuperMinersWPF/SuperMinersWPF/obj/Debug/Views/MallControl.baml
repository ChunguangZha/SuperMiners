   M S B A M L   `   `   `  ����  SuperMinersWPF&     SuperMinersWPF.Views.MallControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5          � 	Resources  5	       !  RightTitleMouseOverBackground �� Color  RadioButtonStyle1 �� 
TargetType �� Property �� Value �� 
TargetType �Name J� SnapsToDevicePixels ��Name	 �� Triggers
 P� Property P� Value �� 
TargetName  MainPanelBackground��%�  &	        &	 #     7   7  7  7  7  �� 5
   
   $ 	#FFC8ECE7��6@   �� 5   
   " 	�6*   .�5      ��  � �� 5      �� 5      " 	�6&   .R�5      � $ templateRoot��6   
������  . .+�5      �� $ contentPresenter��6/   $��1��6I   |�.  #��z��6k   #��z��6�   ��. # z 6�   #��z��6(  5      	 5      P� 5      
 � �� . . �5   "   ��  � ��$ 4,-1,0,0q�6<   5      P� 5      
 �@J�IsMouseOver . . �5   "   �� $ templateRoot��6?   8    � ��6Y   5      P� 5      
 � {� .  . �5   "   �� $ templateRoot��6?   8   � ��6Y   5      P� 5      
 � �� . . �5   "   �� $ templateRoot��6?   8   � ��6Y   5      P� 5      
 �@d�	IsChecked$
 trueY�6;   . �5    "   �� $ templateRoot��6?   8   � ��6Y   5!      P� 5"       6;   Z� 
 �@d�	IsChecked. �5#   "   �� $ templateRoot��6?   8   � ��6Y   5$      5&      5(      5)      5*      .��5+      � .+�5-   
   �� .@�5.      �� ��5/      �� 50      $��积分商城��6    $
��20,5q�6,    J� RenderTransformOrigin$ 0.5,0.5&�6:   z�51      V� 52      .~�53   "   �� �� 54   "   �� 55   "   T� 56   "    T� X$ 0Q�65   57      59      .��5;      � .+�5<      �� -   5         ��Name$ lvVirtualMall��5<       �� SelectionMode$ Single=�66   $��Disabled=�6M   ��5=      �� 5>      .R�5?   "   �  K� IsItemsHost . 6,   5@      ��5B      �� 5C      .R�5D   "   �� $	��300��6)   $��10q�65   $��1q�6A   
����ح� � ToolTip 6m   �� *Remark+ � Triggers 5E   &   :� 5F   *    :� RoutedEvent$! FrameworkElement.MouseEnter��67   .[�5G   .   �� .��5H   2   �� .��5J   6   W�  W� From$ 0X�6F   	 W� To$ 1X�6O    g� Duration$ 0:0:0.1D�6V    �� 
TargetName$ 	borderBuy��6i    �� TargetProperty$ Opacity�6�   5K   3   5L   /   5M   +   :� 5N   *   $! FrameworkElement.MouseLeave��67   .[�5O   .   �� .��5P   2   �� .��5R   6   W� $ 1X�6F   $ 0X�6O   $ 0:0:0.1D�6V   $ 	borderBuy��6i   $ Opacity�6�   5S   3   5T   /   5U   +   ._�5W   &   �  � RowDefinitions 5X   *   �� 5Y   .   $
��Auto �6<   �� 5Z   .   $
��Auto �6<   �� 5[   .   $
��Auto �6<   �� 5\   .   $
��Auto �6<   �� 5]   .   $
��Auto �6<   .+�5_   *   � $��0��6/   $��Disabled=�6<   .+�5`   .   �� $	��180��64   $	��180��6@   $��5q�6M   $��Center=�6X   $��Center=�6s   ��6�   �� *Icon+�� 5a   .   $��borderRemark��65   ���	#FFB83CDC J� Opacity$ 0Q�6`   ._�5b   2   �� $��3��6<   
������� �� Padding$ 10q�6t   ��6I   �� *Remark+6t   5c   /   5d   +   �� 5e   *   $��1��64   $��16(�6A   $��5q�6O   
���  ��$��Center=�6k   �� 5f   .   $�	积分：��62    �� 5g   .   �62   �� *ValueShoppingCredits+5h   +   � 5i   *   $��2��6/   .+�5j   .   �� $
��10,5q�6N   $��Center=�6\   ��68   �� *Name+6\   �� 5k   .   $��	borderBuy��65   $
��10,5q�6F   
�������$ 0Q�6g   ._�5l   2   �� -   5        $	��100��5l   9   $��购买��6E   $��Center=�6R   $��Center=�6m   6�   5m   /   5n   +   5o   '   5p   #   5q      5s      5t      5u      �� 5v      ��5w      �� 5x      $��钻石商城��6    $
��20,5q�6,   .��5z      �  � ColumnDefinitions 5{      �� 5|      $
��Auto �6+   �� 5}      $��* �6+   .+�5      �� .+�5�      	� -   5         	�Name$ rbtnLiveThing��5�   &   $��生活用品��6=   $��10q�6L    d� 	IsChecked$
 TrueY�6Y   $	��Top=�6j   #��[ 6�   $��0q�6�     	� 	GroupName$  	ShowPanel��6�   6�   	� 5�      -   5        $ 
btnDigital��5�   &   $��数码产品��6:   $��10q�6I   $	��Top=�6V   #��[ 6n   $��0q�6�   $  	ShowPanel��6�   6�   	� 5�      -   5        $ btnFood��5�   &   $��食品专区��67   $��10q�6F   $	��Top=�6S   #��[ 6k   $��0q�6�   $  	ShowPanel��6�   6�   	� 5�      -   5        $ btnHomeAppliances��5�   &   $��家用电器��6A   $��10q�6P   $	��Top=�6]   #��[ 6u   $��0q�6�   $  	ShowPanel��6�   6�   	� 5�      -   5        $ btnPhoneFee��5�   &   $��话费充值��6;   $��10q�6J   $	��Top=�6W   #��[ 6o   $��0q�6�   $  	ShowPanel��6�   6�   5�      �� 5�      -	   5        $ lvDiamondMall��5�   /   $��1��6   $ Single=�6F   $��Disabled=�6]   ��5�      �� 5�      .R�5�   "   �  . 6,   5�      ��5�      �� 5�      .R�5�   "   �� -
   5        $	��300��5�   )   $��10q�65   $��1q�6A   
����ح� 6m   �� *Remark+._�5�   %   �  5�   *   �� 5�   .   $
��Auto �6<   �� 5�   .   $
��Auto �6<   �� 5�   .   $
��Auto �6<   �� 5�   .   $
��Auto �6<   �� 5�   .   $
��Auto �6<   .+�5�   *   � $��0��6/   $��Disabled=�6<   .+�5�   .   �� $	��180��64   $	��180��6@   $��5q�6M   $��Center=�6X   $��Center=�6s   ��6�   �� *Icon+�� 5�   .   $��borderRemark��65   ���	#FFB83CDC$ 0Q�6`   ._�5�   2   �� $��3��6<   
�������$ 10q�6t   ��6I   �� *Remark+6t   5�   /   5�   +   �� 5�   *   $��1��64   $��16(�6A   $��5q�6O   
���  ��$��Center=�6k   �� 5�   .   $�	钻石：��62    �� 5�   .   �62   �� *ValueDiamonds+! �� Mode$! OneWay=�5�   +   � 5�   *   $��2��6/   .+�5�   .   �� $
��10,5q�6N   $��Center=�6\   ��68   �� *Name+6\   5�   +   5�   '   5�   #   5�      5�      5�      5�      �� 5�      ��5�      �� 5�      $��我的订单��6    $
��20,5q�6,   5�      �� 5�      ��5�      �� 5�      $��收件地址��6    $
��20,5q�6,   .��5�      �  5�      �� 5�      $��* �6(   �� 5�      $
��Auto �6(   .+�5�      �� -   5        $��lvPostAddress��5�      $��10q�64   
�������
������ $��Stretch=�6l   $
��Auto=�6�   $ Single=�6�   ��5�      �� 5�      .R�5�   "   � $��5q�6'    5�   &   �� 5�   *   �� 5�   *   �� 5�   *   �� 5�   *   �� 5�   *   �� 5�   *   �� 5�   *    5�   &   �� 5�   *   �� 5�   *   �� 5�   *   .+�5�   &   �� $��收件人：��60   $��Center=�6<   $
��Left=�6W   �� 5�   &   $��1��6.   $��2��6>   " �� 
IsReadOnly" . $��Center=�6�   ��6R   �� *ReceiverName+$! OneWay=�6�   �� 5�   &   $��3��60   $��联系电话：��6@   $��Center=�6M   $��Center=�6h   �� 5�   &   $��4��6.   $��3��6>   " . $��Center=�6�   ��6R   �� *PhoneNumber+$! OneWay=�6�   �� 5�   &   $��1��60   $��详细地址：��6=   $��Center=�6J   $
��Left=�6e   �� 5�   &   $��1��6.   $��1��6;   " . $��Center=�6�   ��6K   �� *
Province+$! OneWay=�6�   �� 5�   &   $��1��60   $��2��6=   $��省（市/区）��6M   $��Center=�6[   $��Center=�6v   �� 5�   &   $��1��6.   $��3��6;   " . $��Center=�6�   ��6K   �� *City+$! OneWay=�6�   �� 5�   &   $��1��60   $��4��6=   $��地/市��6M   $��Center=�6X   $��Center=�6s   �� 5�   &   $��1��6.   $��5��6;   " . $��Center=�6�   ��6K   �� *County+$! OneWay=�6�   �� 5�   &   $��1��60   $��6��6=   $��县（市/区）��6M   $��Center=�6[   �� 5�   &   $��2��6.   $��1��6;   $��6��6K   " . $��Center=�6�   ��6_   �� *DetailAddress+$! OneWay=�6�   5�   #   5�      5�      �� 5�      -   5        $��btnDeleteAddress��5�   *   $��1��6   $��删除地址��6B   $	��100��6Q   $��10q�6]   $��Center=�6i   $��Right=�6�   6�   �� 5�      -   5        $��btnUpdateAddress��5�   *   $��1��6   $��修改地址��6B   $	��100��6Q   $��10,10,130,10q�6]   $��Center=�6s   $��Right=�6�   6�   �� 5�      -   5        $��btnAddNewAddress��5�   *   $��1��6   $��添加地址��6B   $	��100��6Q   $��10,10,250,10q�6]   $��Center=�6s   $��Right=�6�   6�   5�      5�      5�      5�      5�      