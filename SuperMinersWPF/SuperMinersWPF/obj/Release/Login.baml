   M S B A M L   `   `   `  ����   SuperMinersWPF    SuperMinersWPF.Login   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -   $	��270��$	��347��  � 
ResizeMode$  NoResize=� � WindowStartupLocation$ CenterScreen=� � Icon$ Logo128.ico�� � WindowStyle$
 None=�.���  � ColumnDefinitions �� $��70 ��� $	��33* ��� $
��Auto � � RowDefinitions �� $��23 ��� $	��104 ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $	��95* �.+�� $��2��$��3��$��0,0,0,10q�����  �� ImageSource$ Resources/login.png��.+��� $��24(�$��Center=�$��Center=�
���   ��� $�欢迎加入�� �� ��� *Strings.Title+ �� Mode$ OneWay=�   Language �� Source# [  � $��3��.+��� -   $��btnClose��
������ $��0q�$��5q�$��Right=�$	��Top=���F� +����        р�� ��� Ѐ��     �    ��� 	
�������$��2���� $��2��$��0��$��账号:��$��20,0,0,0q�$��Right=�$��Center=�$��14(��� -   $��txtUserName��$��2��$��1��$
��10,5q�	 �� 	MaxLength$	 50��$��18(��� -   $��txtRegister��$��2��$��2��$��Center=�$��Center=�$��0,0,10,0q��� -   
 �Name$
 hlinkRegister��$��http://goto.xlore.net/?�.�快速注册�� $��3��$��0��$��密码:��$��20,0,0,0q�$��Right=�$��Center=�$��14(�G� -   $��txtPassword��$��3��$��1��$
��10,5q� G� 	MaxLength$ 50��$��18(��� $��3��$��2��$��Center=�$��Center=�$��0,0,10,0q��� -   $
 hlinkForgetPassword��.�忘记密码�� -   $��btnLogin��$��5��$��1��$��登录��$	��100��$��30��$��Center=�$��Center=� �� 	IsDefault . ���	#FFF78F75
�������$��14(��� -	   $��chkRememberPwd��$��5��$��0��$��2��$��记住密码��$
��20,5q�$��Center=�$
��Left=��� $��5��$��2��$��Center=�$��Center=�$��0,0,10,0q��� -
   $
 hlinkHomePage��$��http://goto.xlore.net/?�.�矿场主页