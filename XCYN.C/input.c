#include <stdio.h>
#include <stdlib.h>
#include <Windows.h>
#include <conio.h>

//int Fun6() {
int Fun6() {
	int a, b = 0;
	//&��Ϊȡ��ַ����Ҳ���ǻ�ȡ�������ڴ��еĵ�ַ
	//scanf()���� printf() ���ƣ�scanf() ��������������͵����ݡ�
	//scanf("%d %d", &a, &b);
	//printf("a + b = %d\n", a + b);

	//��ӡ��ַ
	//char c = ' ';
	//printf("a& = %p b& = %p c& = %p\n", &a, &b, &c);

	//getchar()��scanf()�����Ʒ
	//char d = getchar();
	//printf("d = %c\n", d);

	//_getche()������getchar()����������û�л����������ð�Enter����������ڿ���̨��
	//char e = _getche();
	//printf("e = %c\n", e);

	//_getch()������_getche()�������������ڣ�����������û��������ַ�������Щ������Ҫ�������ԣ�������������͵÷�ֹ����͵��
	//char f = _getch();
	//printf("f = %c\n", f);

	//gets()������scanf()����������gets()ֻ�������ַ������Ͳ��ҿ��Դ�ӡ�ո�scanf()��������������ͣ������ܴ�ӡ�ո�
	//char g[20];
	//gets(g);
	////scanf("%s", g);
	//printf("g = %s\n", g);
	ZSSJT();
	//system("pause");
	return 0;
}

//����ʽ����
int ZSSJT() {
	char ch;
	int i = 0;
	while (ch = _getch()) {
		if (ch == 27) {
			break;
		}
		else {
			printf("Number��%d\n", ++i);
		}
	}
	return 0;
}

//������ʽ����
int FZSSJT() {
	char ch;
	int i = 0;
	while (1) {
		if (_kbhit()) {	//��⻺�������Ƿ�������
			ch = _getch();	//���������е��������ַ�����ʽ����
			if (ch == 27) {
				break;
			}
			else {
				printf("Number��%d\n", ++i);
			}
		}
		Sleep(1000);  //��ͣ1��
	}
	return 0;
}