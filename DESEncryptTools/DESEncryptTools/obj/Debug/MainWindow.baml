   M S B A M L   `   `   `  ����  DESEncryptTools     DESEncryptTools.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$!  字符串加密解密工具��5   	   $	��350��6   $	��525��6(   .��5      �  � RowDefinitions 5   
   �� 5      $	��14* �6   �� 5      $	��15* �6   .+�5
   
   �� $��加密��6   .��5      ��  �� Orientation$ Vertical=�6   .+�5      �� $ 
Horizontal=�6   .+�5      �� $��原始字符串：��6    $	��100��6.   $��10q�6:   �� 5      -   5        $��txtSrcString_ToEncrypt��5      $	��300��6<   $��50��6H   $	��Top=�6T    �� TextWrapping$
 Wrap=�6l   $��10q�6�   5      �� 5      $ 
Horizontal=�6   .+�5      �� -   5        $��
btnEncrypt��5      $��加密��6/   $	��100��6<   $	��Top=�6H   $��10q�6`   6l   �� 5      -   5        $��txtDescString_ToEncrypt��5      $	��300��6=   $��50��6I   $	��Top=�6U   $
 Wrap=�6m   $��10q�6�   5      5      5      �� 5   
   $��解密��6   $��1��6   .��5      �� $ Vertical=�6   .+�5      �� $ 
Horizontal=�6   .+�5      �� $��原始字符串：��6    $	��100��6.   $��10q�6:   �� 5      -   5        $��txtSrcString_ToDecrypt��5      $	��300��6<   $��50��6H   $	��Top=�6T   $
 Wrap=�6l   $��10q�6�   5      �� 5      $ 
Horizontal=�6   .+�5      �� -   5        $��
btnDecrypt��5      $��解密��6/   $	��100��6<   $	��Top=�6H   $��10q�6`   6l   �� 5      -   5        $��txtDescString_ToDecrypt��5      $	��300��6=   $��50��6I   $	��Top=�6U   $
 Wrap=�6m   $��10q�6�   5      5       5!      5"      5#      