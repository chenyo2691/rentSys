   M S B A M L   `   `   `  ����B  >rentSys, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:rentSys.Views.roomrentSys.Views.room  5      -clr-namespace:rentSys.ViewsrentSys.Views  5	      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5
          rentSys.Views.room.index   5      *local clr-namespace:rentSys.Views.room  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;Controls,http://metro.mahapps.com/winfx/xaml/controls  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      Y UMahApps.Metro.IconPacks.Modern, Version=1.9.0.0, Culture=neutral, PublicKeyToken=null^ ZMahApps.Metro.IconPacks.FontAwesome, Version=1.9.0.0, Culture=neutral, PublicKeyToken=nullY	 UMahApps.Metro.IconPacks.Entypo, Version=1.9.0.0, Culture=neutral, PublicKeyToken=null[
 WMahApps.Metro.IconPacks.Material, Version=1.9.0.0, Culture=neutral, PublicKeyToken=nullC	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks   	 
 5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006   	 
 5      1	metroDemoclr-namespace:rentSys.Views   	 
 5	      ;assembly&clr-namespace:System;assembly=mscorlib   	 
 5
      -   5          � 	Resources  5         	keyStatus S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� Type"   rentSys.Views.room.statusOpt��%j   &	        S� 5   
   $ 	GetValues��6/   "  6F    5      M� 5      "  6   62   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   `� $��0��6   .�5      c� .@�5      �� -   5        $��新增��5      6&   �� 5      -   5        $��修改��5      6&   �� 5      -   5        $��删除��5      6&   5       5!      &   System.Windows.Controls.DataGrid  5$   
   -   5        $
��grid��5$      $��1��6      AutoGenerateColumns .     
IsReadOnly .     SelectionMode$ Extended=�6s   ��6,   �� 	   Columns	 5%      4  .System.Windows.Controls.DataGridCheckBoxColumn  5&        MetroDataGridCheckBox1  +System.Windows.Controls.DataGridBoundColumn
   ElementStyle#
 �  6)      EditingElementStyle# �  5'   )   ,  &System.Windows.Controls.DataGridColumn   Header$ 操作��5(   )      Binding 5)   )   ��  �� RelativeSource ��  �� AncestorType$ DataGridRowL� �� Path$ 
IsSelected� �� Mode$ OneWay=�0  *System.Windows.Controls.DataGridTextColumn  5+         Width5  /System.Windows.Controls.DataGridLengthConverter$ 80 6%   $ 编号��60    6<   �� *id+  5,      $ 80 6%   $ 房号��60    6<   �� *number+  5-      $ 80 6%   $ 楼层��60    6<   �� *floor+8	  2MahApps.Metro.Controls.DataGridNumericUpDownColumn	  5.      $ 80 67   $ 房租��6B    	  StringFormat$ C��6h    	  Minimum$ 0Q�6y    6N   �� *price+6y     5/      $ 80 6%   $ 备注��60    6<   �� *remark+4
  .System.Windows.Controls.DataGridTemplateColumn
  50      $ 80 6)   $ 状态��64    
  CellTemplate 51      �� 52      .R�53      �� ��6(   �� $ 状态�55       
  CellEditingTemplate 57      �� 58      .R�5:      ��  ��Name$ 	taskCombo��6'   $ �� IsSynchronizedWithCurrentItem$ FalseY�6�   ��6:   ��  �� Source# [   �� SelectedItem 6u   �� $ 状态�6�   5;      5=        5>      $ 80 6%   $ 状态��60    6<   �� *status+5@      5A      5B      