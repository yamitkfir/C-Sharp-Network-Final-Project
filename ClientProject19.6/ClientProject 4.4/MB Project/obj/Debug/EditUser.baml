   M S B A M L   `   `   `  ����H  DClientProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ClientProjectClientProject  5   	       ClientProject.EditUser   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ClientProject     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � Title$
  User��5	   	   $	��450��6   $	��800��6#   .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��1��6   $��0��6    $��Segoe UI Black)�6-   $��35(�6I   $��Center=�6W   $��Center=�6r   .��6�   USER6�   �� 5   
   -   5        $��upload��5      $��3��6   $��4��6/   .��6Q   upload a picture6c   �� 5   
   -   5        $
��save��5      $��2��6   $��4��6-   .��6M   
save image6Y   �� 5   
   $��2��6   $��2��6    $��3��64   $��Center=�6A   $��Bottom=�6^   $?��9please upload your image in order to see it right here :)��6y   �� 5    
   -   5        $��	userimage��5       $��UniformToFill=�6!   $��2��69   $��2��6I   $��0��6]   $��4��6j   5!      �� 5#   
   -   5        $��saveall��5#      $��0��6    $��0��61   $��Stretch=�6T   $��Bottom=�6r   .��6�   save information6�   �� 5%   
   $��Segoe UI Black)�6   $��30(�6,   $��1��6:   $��Right=�6G   $��Bottom=�6c   .��6~   name:6�   �� 5&   
   $��Segoe UI Black)�6   $��30(�6,   $��2��6:   $��Right=�6G   $��Bottom=�6c   .��6~   	family:6�   �� 5'   
   $��Segoe UI Black)�6   $��30(�6,   $��3��6:   $��Right=�6G   $��Bottom=�6c   .��6   age:6�   �� 5(   
   $��Segoe UI Black)�6   $��30(�6,   $��4��6:   $��Right=�6G   $��Bottom=�6c   .��6   	gender:6�   �� 5)   
   -   5        $
��name��5)      $��Segoe UI Black)�6   $��30(�68   $��1��6F   $��1��6S   6e   �� 5*   
   -   5        $��family��5*      $��Segoe UI Black)�6   $��30(�6:   $��2��6H   $��1��6U   6g   �� 5+   
   -   5        $	��age��5+      $��Segoe UI Black)�6   $��30(�69   $��3��6G   $��1��6T   6f   �� 5,   
   -	   5        $��gender��5,      $��Segoe UI Black)�6!   $��30(�6=   $��4��6K   $��1��6X   .@�5-      �� $��Female��6   �� 5.      $
��Male��6   �� 5/      $��Unspecified��6   50      �� 54   
   -
   5        $
��Left=�54   (   $	��Top=�6C      MaterialDesignFlatButton#��[  6[   .��55      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Close=�6&   56      59      5:      