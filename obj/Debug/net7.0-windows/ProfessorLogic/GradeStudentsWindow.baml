   M S B A M L   `   `   `  ����  	TRPZ_Kurs2    ,TRPZ_Kurs.ProfessorLogic.GradeStudentsWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Grade Students��5   	   $	��450��6    $	��800��6-    � 	Resources 5       �� 
TargetType �� Property �� Value&   System.Windows.Controls.DataGrid=  7System.Windows.Controls.Primitives.DataGridColumnHeader*  $System.Windows.Controls.DataGridCell��%�  '��       '�� �     '  1    �� 5   
   " ��6   .�5      ��  � ��$ 16(�6)   �� 5       � ��$ Center=�62   �� 5	       � ��$ 0,0,10,0q�6'   5
      �� 5   
   " ��6   .�5      ��  � ��$	 150��6&   �� 5       � ��$ 16(�6)   �� 5       � ��$ 0,0,10,0q�6'   5      �� 5   
   "  6   .�5      ��  � ��$ 10q�6'   �� 5       � ��$ 14(�6)   �� 5       �@ RowBackground �#F5F5F56.   �� 5        �@ AlternatingRowBackground �#FFFFFF69   �� 5       � �� �#CCCCCC6,   �� 5       � ��$ 1q�60   �� 5       �@ CanUserAddRows$ False��6/   �� 5       �@ AutoGenerateColumns$ False��64   �� 5       �@ SelectionMode$ Single=�6.   �� 5       �@ SelectionUnit$ FullRow=�6.   �� 5       �@ HeadersVisibility$ Column=�62   �� 5       �@ GridLinesVisibility$ 
Horizontal=�64   �� 5       �@ ColumnHeaderStyle 5      �� 5      "  6   .�5       ��  � �� �#2196F367   �� 5!       � ��
 �����67   �� 5"       � ��$
 Bold"�67   �� 5#       � ��$ 30��63   �� 5$       � ��$ 14(�65   �� 5%       � ��$ Center=�6G   �� 5&       � ��$ Center=�6E   �� 5'       � ��$ 0,0,1,1q�6<   �� 5(       � ��
 �����68   5)      5+      �� 5,       �@ 	CellStyle 5-      �� 5.      "  6   .�5/      ��  � ��$ 0,0,1,1q�6<   �� 50       � �� �#CCCCCC68   �� 51       � ��$ 30��63   �� 52       � ��$ 5q�64   �� 53       � ��$ Center=�6E   �� 54       � ��$
 Left=�6G   55      57      58      59      .��5:      �  � RowDefinitions 5;   
   �� 5<      $
��Auto �6   �� 5=      $��* �6   .+�5@   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��10q�6;   .+�5A      �� $��Select Group:��6   $��Center=�6-   $��0,0,10,0q�6H   �� 5B      -   5         ��Name$ GroupComboBox��5B      $	��150��6.   6:   5C        5E   
   -   5          Name$ StudentsDataGrid��5E      $��1��6-   	   Columns	 5F      0  *System.Windows.Controls.DataGridTextColumn  5G      ,  &System.Windows.Controls.DataGridColumn
   Header$
 
Student ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6W   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *	StudentID+6W     5H      $
 
First Name��6%   $
 Auto 6W    69   �� *	FirstName+6W     5I      $
 	Last Name��6%   $
 Auto 6U    68   �� *
LastName+6U     5J      $
 
Subject ID��6%   $
 Auto 6W    69   �� *	SubjectID+6W     5K      $
 Subject Name��6%   $
 Auto 6[    6;   �� *SubjectName+6[     5L      $
 Grade��6%   $
 Auto 6N    64   �� *Grade+6N   5N      5O      5P      