#include <stdio.h>
#include <string.h>

int Fun25() {
	int arr[] = {10, 20, 30, 40, 50};
	int length = sizeof(arr) / sizeof(int);
	printf("�����ش�ӡ�����е�Ԫ��\n");
	for (int i = 0; i <= length - 1; i++)
	{
		//�����ش�ӡ�����е�Ԫ��
		printf("%d\n", arr[i]);
	}
	printf("ʹ������ָ���ӡ�����е�Ԫ��\n");
	for (int i = 0; i <= length - 1; i++)
	{
		//ʹ������ָ���ӡ�����е�Ԫ��
		printf("%d\n", *(arr + i));	//arr ��������һ��ָ�룬����ֱ�Ӹ�ֵ��ָ����� p��arr ������� 0 ��Ԫ�صĵ�ַ
	}
	int *p = &arr[0];
	printf("ʹ������ָ�루�����һ��Ԫ�ص��±꼴�����±꣩��ӡ�����е�Ԫ��\n");
	for (int i = 0; i <= length - 1; i++)
	{
		//ʹ������ָ�루�����һ��Ԫ�ص��±꼴�����±꣩��ӡ�����е�Ԫ��
		printf("%d\n", *(p + i));
	}
	printf("��������arr��������ָ�루p��������arr�ǳ�����p�Ǳ�����p��������1");
	for (int i = 0; i <= length - 1; i++)
	{
		//������ (arr) �ǳ���������ֵ���ܸı䣬������ָ�� (p) �Ǳ����������ر�ָ�����ǳ�������
		//����ֵ��������ı䡣Ҳ����˵��������ֻ��ָ������Ŀ�ͷ��
		//������ָ�������ָ�����鿪ͷ����ָ������Ԫ�ء�
		printf("%d\n", *(p++));
	}
	return 0;
}

int Fun26() {
	//C���������ֱ�ʾ�ַ����ķ�����ʹ���ַ������ʹ���ַ���������ǰ�߿��Զ�ȡ���޸ģ�����ֻ�ܶ�ȡ
	//��ԭ���ַ�����洢��ȫ����������ջ�����ڶ�����ʽ���ַ����洢�ڳ�������ȫ����������ջ�����ַ�����Ҳ�����������ݣ��ж�ȡ��д���Ȩ�ޣ������������ַ�����Ҳ�����������ݣ�ֻ�ж�ȡȨ�ޣ�û��д��Ȩ�ޡ���
	char str[] = "http://c.biancheng.net";
	char *str2 = "http://c.biancheng.net";
	printf("str��%s", str);
	printf("\n");
	printf("str2��%s", str2);
	printf("\n");
	int length = strlen(str);
	printf("ʹ���ַ������������");
	for (int i = 0; i < length; i++) {
		printf("%c", str[i]);
	}
	return 0;
}

int Fun27() {
	char str[] = "http://c.biancheng.net";
	char *str2 = "http://c.biancheng.net";
	str[4] = 's';
	printf("str��%s", str);	//��:�ĳ�s
	printf("\n");
	//str2[4] = 's';
	//printf("str2��%s", str2);	//��:�ĳ�s������str2��һ������������ִ�е�����ᱨ�����������쳣: д�����Ȩ�޳�ͻ����
	str2 = "http://www.baidu.com";	//����һ��������ֵ��str2����������ָ������һ���ڴ��ָ��
	printf("str2��%s", str2);
	return 0;
}