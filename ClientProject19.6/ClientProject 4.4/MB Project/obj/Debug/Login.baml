   M S B A M L   `   `   `  ����H  DClientProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ClientProjectClientProject  5   	       ClientProject.Login   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ClientProject     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  login��5	   	   $	��450��6   $	��800��6$    � 
ResizeMode$ NoResize=�5
   	    � WindowStartupLocation$ CenterScreen=�6   .��5      �� .+�5   
   �� 
 �� Top$	 -20��6   $	��470��6!   $	��850��6.    �� Left$	 -30��6:   ��5      �� 5       �� 	UriSource$ images\login_page.jpg?�6   5      �� 5   
    �� Orientation$ 
Horizontal=�6   $	 200��6.   $ 65��6@   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Account=�6&   $��Center=�65   ���#707070$��60��6e   $��60��6q   �� 5      -   5         ��Name$ account��5      $��36(�6'   )  #MaterialDesignThemes.Wpf.HintAssist	   Hint$	  account��66   $	��220��6`   5      �� 5   
   $ 
Horizontal=�6   $	 200��6.   $	 125��6@   .+�5        $	 Key=�6&   $��Center=�61   ���#707070$��60��6a   $��60��6m   G� 5      -   5        $��password��5      $��36(�6*   $	 	 password��68   $	��220��6c   5      �� 5   
   -   5        
 ��Name$
 login_label��5      $��David)�6%   $��36(�68   $��47��6F   $	��636��6R   $	 147��6^   $ 11��6p   
�������$.��(LOGIN: please enter account and password��6�   �� 5   
   -   5        $��l1��5      $��35��6   $	��226��6&   $ 30��62   $	 376��6C   6V   �� 5   
   -   5        $��enter��5      $��David)�6   $��36(�61   $��75��6?   $	��165��6K   $	 525��6W   $ 95��6i   .��6�   LOG IN6�   �� 5   
   -   5        $��register��5      $��David)�6!   $��25(�64   $��41��6B   $	��155��6N   $ 30��6Z   $	 189��6k   .��6�   
REGISTER6�   �� 5!   
   -   5        $��Right=�5!         MaterialDesignFlatButton#��[  69   .��5"        $ Close=�6&   5#      �� 5$   
   -   5        $��return��5$      $ 50��64   $ 10��6D   $��return to previous page��6U   5&      5'      