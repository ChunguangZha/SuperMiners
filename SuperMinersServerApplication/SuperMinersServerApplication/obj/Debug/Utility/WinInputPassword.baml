   M S B A M L   `   `   `  ����   SuperMinersServerApplication;    5SuperMinersServerApplication.Utility.WinInputPassword   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$   ��5   	   $	��200��6   $	��350��6    � 
ResizeMode$ NoResize=�6+    � WindowStartupLocation$ CenterOwner=�6A    � ShowActivated .   � Topmost .  � WindowStyle$ 
ToolWindow=�6�   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $	��22* �6   .+�5   
   �� $��请输入密码：��6   $��10q�6"   $
��Left=�6.   $��16(�6I   G� 5   
   -   5         G�Name$ txtPassword��5      $��1��6+   $��10q�68   $	��Top=�6D   $��16(�6\   �� 5   
   -   5        $��btnOK��5      $��2��6   $��确定��6+   $
��20,5q�68   $��Right=�6G   $	��Top=�6c   $��10,10,100,10q�6{    �� 	IsDefault . 6�   �� 5   
   -   5        $��	btnCancel��5      $��2��6   $��取消��6/   $
��20,5q�6<   $��Right=�6K   $	��Top=�6g   $��10q�6   	 �� IsCancel	 . 6�   5      5      