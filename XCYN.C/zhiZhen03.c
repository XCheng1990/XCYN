#include <stdio.h>
#include <string.h>

int Fun25() {
	int arr[] = {10, 20, 30, 40, 50};
	int length = sizeof(arr) / sizeof(int);
	printf("正常地打印数组中的元素\n");
	for (int i = 0; i <= length - 1; i++)
	{
		//正常地打印数组中的元素
		printf("%d\n", arr[i]);
	}
	printf("使用数组指针打印数组中的元素\n");
	for (int i = 0; i <= length - 1; i++)
	{
		//使用数组指针打印数组中的元素
		printf("%d\n", *(arr + i));	//arr 本身就是一个指针，可以直接赋值给指针变量 p。arr 是数组第 0 个元素的地址
	}
	int *p = &arr[0];
	printf("使用数组指针（数组第一个元素的下标即数组下标）打印数组中的元素\n");
	for (int i = 0; i <= length - 1; i++)
	{
		//使用数组指针（数组第一个元素的下标即数组下标）打印数组中的元素
		printf("%d\n", *(p + i));
	}
	printf("数组名（arr）与数组指针（p）的区别：arr是常量，p是变量，p可以自增1");
	for (int i = 0; i <= length - 1; i++)
	{
		//数组名 (arr) 是常量，它的值不能改变，而数组指针 (p) 是变量（除非特别指明它是常量），
		//它的值可以任意改变。也就是说，数组名只能指向数组的开头，
		//而数组指针可以先指向数组开头，再指向其他元素。
		printf("%d\n", *(p++));
	}
	return 0;
}

int Fun26() {
	//C语言有两种表示字符串的方法：使用字符数组和使用字符串常量，前者可以读取和修改，后者只能读取
	//（原因：字符数组存储在全局数据区或栈区，第二种形式的字符串存储在常量区。全局数据区和栈区的字符串（也包括其他数据）有读取和写入的权限，而常量区的字符串（也包括其他数据）只有读取权限，没有写入权限。）
	char str[] = "http://c.biancheng.net";
	char *str2 = "http://c.biancheng.net";
	printf("str：%s", str);
	printf("\n");
	printf("str2：%s", str2);
	printf("\n");
	int length = strlen(str);
	printf("使用字符串数组输出：");
	for (int i = 0; i < length; i++) {
		printf("%c", str[i]);
	}
	return 0;
}

int Fun27() {
	char str[] = "http://c.biancheng.net";
	char *str2 = "http://c.biancheng.net";
	str[4] = 's';
	printf("str：%s", str);	//将:改成s
	printf("\n");
	//str2[4] = 's';
	//printf("str2：%s", str2);	//将:改成s，由于str2是一个常量，所以执行到这里会报错（引发了异常: 写入访问权限冲突。）
	str2 = "http://www.baidu.com";	//将另一个常量赋值给str2，这样它就指向了另一个内存的指针
	printf("str2：%s", str2);
	return 0;
}