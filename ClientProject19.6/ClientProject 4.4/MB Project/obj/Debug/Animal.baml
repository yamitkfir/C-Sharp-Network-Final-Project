   M S B A M L   `   `   `  ����H  DClientProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ClientProjectClientProject  5   	       ClientProject.Animal   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ClientProject     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Animal��5	   	   .��5
      �  � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��1��6   $��0��6    $��Segoe UI Black)�6-   $��25(�6I   $��Center=�6W   $��Center=�6r   $��EDIT ANIMAL��6�   �� 5   
   -   5        $��upload��5      $��3��6   $��4��6/   .��6Q   upload a picture6c   �� 5   
   -   5        $
��save��5      $��2��6   $��4��6-   .��6M   
save image6Y   �� 5   
   $��2��6   $��2��6    $��3��64   $��Center=�6A   $��Bottom=�6^   $H��Bplease upload your animal's image in order to see it right here :)��6y   �� 5   
   -   5        $��animalimage��5      $��UniformToFill=�6#   $��2��6;   $��2��6K   $��0��6_   $��4��6l   5       �� 5!   
   -   5        $��saveall��5!      $��0��6    $��0��61   $��Stretch=�6T   $��Bottom=�6r   .��6�   save information6�   �� 5#   
   $��Segoe UI Black)�6   $��30(�6,   $��1��6:   $
��Left=�6G   $	��Top=�6b   .��6z   name:6�   �� 5$   
   $��Segoe UI Black)�6   $��30(�6,   $��2��6:   $
��Left=�6G   $	��Top=�6b   .��6z   	family:6�   �� 5%   
   $��Segoe UI Black)�6   $��30(�6,   $��3��6:   $
��Left=�6G   $	��Top=�6b   .��6{   age:6�   �� 5&   
   $��Segoe UI Black)�6   $��30(�6,   $��4��6:   $
��Left=�6G   $	��Top=�6b   .��6{   	gender:6�   �� 5'   
   $��Segoe UI Black)�6   $��15(�6,   $��1��6:   $��1��6G   $
��Left=�6W   $	��Top=�6r   .��6�   how often eats (hours):6�   �� 5(   
   $��Segoe UI Black)�6   $��15(�6,   $��1��6:   $��1��6G   $
��Left=�6W   $��Center=�6r   .��6�   last time ate:6�   �� 5)   
   $��Segoe UI Black)�6   $��15(�6,   $��2��6:   $��1��6G   $
��Left=�6W   $	��Top=�6r   .��6�   how oten goes out (hours):6�   �� 5*   
   $��Segoe UI Black)�6   $��15(�6,   $��2��6:   $��1��6G   $
��Left=�6W   $��Center=�6r   .��6�   last time went out:6�   �� 5+   
   $��Segoe UI Black)�6   $��15(�6,   $��3��6:   $��1��6G   $
��Left=�6W   $	��Top=�6r   .��6�   type:6�   �� 5,   
   $��Segoe UI Black)�6   $��15(�6,   $��3��6:   $��1��6G   $
��Left=�6W   $��Bottom=�6r   .��6�   other notes:6�   �� 5.   
   -   5        $
��name��5.      $��Segoe UI Black)�6   $��30(�68   $��1��6F   $��0��6S   $��Center=�6c   6�   �� 5/   
   -   5        $��family��5/      $��Segoe UI Black)�6   $��30(�6:   $��2��6H   $��0��6U   $��Center=�6e   6�   �� 50   
   -   5        $	��age��50      $��Segoe UI Black)�6   $��30(�69   $��3��6G   $��0��6T   $��Center=�6d   6�   �� 51   
   -   5        $��lastimeaten��51      $��Segoe UI Black)�6%   $��10(�6A   $��1��6O   $��1��6\   $��Center=�6l   $��Right=�6�   $��25��6�   $	��100��6�   �� 52   
   -	   5        $��howoteneats��52      $��Segoe UI Black)�6%   $��10(�6A   $��1��6O   $��1��6\   $	��Top=�6l   $��Right=�6�   $��25��6�   $	��100��6�   6�   �� 53   
   -
   5        $��lastimewent��53      $��Segoe UI Black)�6%   $��10(�6A   $��2��6O   $��1��6\   $��Center=�6l   $��Right=�6�   $��22��6�   $	��100��6�   6�   �� 54   
   -   5        $��howoftenout��54      $��Segoe UI Black)�6%   $��10(�6A   $��2��6O   $��1��6\   $	��Top=�6l   $��Right=�6�   $��25��6�   $	��100��6�   �� 55   
   -   5        $��
othernotes��55      $��Segoe UI Black)�6$   $��10(�6@   $��4��6N   $��0��6[   $��Bottom=�6k   6�   �� 56   
   -   5        $��gender��56      $��Segoe UI Black)�6!   $��20(�6=   $��4��6K   $��0��6X   $��Bottom=�6h   .@�57      �� $��Female��6   �� 58      $
��Male��6   �� 59      $��Unspecified��6   5:      �� 5;   
   -   5        $
��type��5;      $��Segoe UI Black)�6   $��15(�6;   $��3��6I   $��1��6V   $��Center=�6f   .@�5<      �� $	��Dog��6   �� 5=      $	��Cat��6   �� 5>      $��Horse��6   �� 5?      $	��Cow��6   �� 5@      $	��Pig��6   �� 5A      $��Snake��6   �� 5B      $
��Bird��6   �� 5C      $
��Fish��6   �� 5D      $��Unspecified��6   5E      �� 5G   
   -   5        $��	needsfood��5G      $��4��6$   $��1��61   $��Right=�6A   $��Center=�6]    � LayoutTransform 5H      �� 5I       �� ScaleX$ 2Q�6!    �� ScaleY$ 2Q�6,   5K      �� 5L   
   -   5        $��needsout��5L      $��4��6#   $��1��60   $��Right=�6@   $��Bottom=�6\    5M      �� 5N      $ 2Q�6!   $ 2Q�6,   5P      �� 5Q   
   $��Segoe UI Black)�6   $��15(�6,   $��4��6:   $��1��6G   $
��Left=�6W   $��Center=�6r   .��6�   
needs food6�   �� 5R   
   $��Segoe UI Black)�6   $��15(�6,   $��4��6:   $��1��6G   $
��Left=�6W   $��Bottom=�6r   .��6�   needs to go out6�   �� 5U   
   -   5        $
��Left=�5U   (   $	��Top=�6C      MaterialDesignFlatButton#��[  6[   .��5V      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Close=�6&   5W      5Z      5[      