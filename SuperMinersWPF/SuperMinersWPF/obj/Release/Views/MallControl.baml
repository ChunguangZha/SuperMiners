   M S B A M L   `   `   `  ����   SuperMinersWPF&     SuperMinersWPF.Views.MallControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -     � 	Resources   !  RightTitleMouseOverBackground �� Color  RadioButtonStyle1 �� 
TargetType �� Property �� Value �� 
TargetType �Name J� SnapsToDevicePixels ��Name	 �� Triggers
 P� Property P� Value �� 
TargetName  MainPanelBackground��%l  &	        &	      7   7  7  7  7  �� $ 	#FFC8ECE7���� " 	�.���  � �� �� " 	�.R�� $ templateRoot��
������  . .+��� $ contentPresenter��$��1��|�.  #��z��#��z����. # z #��z��	 P� 
 � �� . . ���  � ��$ 4,-1,0,0q�P� 
 �@J�IsMouseOver . . ��� $ templateRoot��8    � ��P� 
 � {� .  . ��� $ templateRoot��8   � ��P� 
 � �� . . ��� $ templateRoot��8   � ��P� 
 �@d�	IsChecked$
 trueY�. ��� $ templateRoot��8   � ��P�  Z� 
 �@d�	IsChecked. ��� $ templateRoot��8   � ��.��� .+��� .@��� ���� $��积分商城��$
��20,5q� J� RenderTransformOrigin$ 0.5,0.5&�z�V� .~��� �� �� T�  T� X$ 0Q�.��� .+��� -    ��Name$ lvVirtualMall�� �� SelectionMode$ Single=�$��Disabled=����� .R��  K� IsItemsHost . ���� .R��� $	��300��$��10q�$��1q�
����ح� � ToolTip �� *Remark+ � Triggers :�  :� RoutedEvent$! FrameworkElement.MouseEnter��.[��� .���� .��W�  W� From$ 0X�	 W� To$ 1X� g� Duration$ 0:0:0.1D� �� 
TargetName$ 	borderBuy�� �� TargetProperty$ Opacity�:� $! FrameworkElement.MouseLeave��.[��� .���� .��W� $ 1X�$ 0X�$ 0:0:0.1D�$ 	borderBuy��$ Opacity�._��  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� $��0��$��Disabled=�.+��� $	��180��$	��180��$��5q�$��Center=�$��Center=����� *Icon+�� $��borderRemark�����	#FFB83CDC J� Opacity$ 0Q�._��� $��3��
������� �� Padding$ 10q����� *Remark+�� $��1��$��16(�$��5q�
���  ��$��Center=��� $�	积分：�� �� ��� *ValueShoppingCredits+� $��2��.+��� $
��10,5q�$��Center=����� *Name+�� $��	borderBuy��$
��10,5q�
�������$ 0Q�._��� -   $	��100��$��购买��$��Center=�$��Center=��� ���� $��钻石商城��$
��20,5q�.���  � ColumnDefinitions �� $
��Auto ��� $��* �.+��� .+�	� -    	�Name$ rbtnLiveThing��$��生活用品��$��10q� d� 	IsChecked$
 TrueY�$	��Top=�#��[ $��0q�  	� 	GroupName$  	ShowPanel��	� -   $ 
btnDigital��$��数码产品��$��10q�$	��Top=�#��[ $��0q�$  	ShowPanel��	� -   $ btnFood��$��食品专区��$��10q�$	��Top=�#��[ $��0q�$  	ShowPanel��	� -   $ btnHomeAppliances��$��家用电器��$��10q�$	��Top=�#��[ $��0q�$  	ShowPanel��	� -   $ btnPhoneFee��$��话费充值��$��10q�$	��Top=�#��[ $��0q�$  	ShowPanel���� -	   $ lvDiamondMall��$��1��$ Single=�$��Disabled=����� .R��  . ���� .R��� -
   $	��300��$��10q�$��1q�
����ح� �� *Remark+._��  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� $��0��$��Disabled=�.+��� $	��180��$	��180��$��5q�$��Center=�$��Center=����� *Icon+�� $��borderRemark�����	#FFB83CDC$ 0Q�._��� $��3��
�������$ 10q����� *Remark+�� $��1��$��16(�$��5q�
���  ��$��Center=��� $�	钻石：�� �� ��� *ValueDiamonds+! �� Mode$! OneWay=�� $��2��.+��� $
��10,5q�$��Center=����� *Name+�� ���� $��我的订单��$
��20,5q��� ���� $��收件地址��$
��20,5q�.���  �� $��* ��� $
��Auto �.+��� -   $��lvPostAddress��$��10q�
�������
������ $��Stretch=�$
��Auto=�$ Single=����� .R�� $��5q� �� �� �� �� �� �� ��  �� �� �� .+��� $��收件人：��$��Center=�$
��Left=��� $��1��$��2��" �� 
IsReadOnly" . $��Center=����� *ReceiverName+$! OneWay=��� $��3��$��联系电话：��$��Center=�$��Center=��� $��4��$��3��" . $��Center=����� *PhoneNumber+$! OneWay=��� $��1��$��详细地址：��$��Center=�$
��Left=��� $��1��$��1��" . $��Center=����� *
Province+$! OneWay=��� $��1��$��2��$��省（市/区）��$��Center=�$��Center=��� $��1��$��3��" . $��Center=����� *City+$! OneWay=��� $��1��$��4��$��地/市��$��Center=�$��Center=��� $��1��$��5��" . $��Center=����� *County+$! OneWay=��� $��1��$��6��$��县（市/区）��$��Center=��� $��2��$��1��$��6��" . $��Center=����� *DetailAddress+$! OneWay=��� -   $��btnDeleteAddress��$��1��$��删除地址��$	��100��$��10q�$��Center=�$��Right=��� -   $��btnUpdateAddress��$��1��$��修改地址��$	��100��$��10,10,130,10q�$��Center=�$��Right=��� -   $��btnAddNewAddress��$��1��$��添加地址��$	��100��$��10,10,250,10q�$��Center=�$��Right=�