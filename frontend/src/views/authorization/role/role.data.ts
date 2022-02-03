import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';
import { statusOptions } from '/@/utils/status';
import { Status } from '/@/utils/status';
import { Tag } from 'ant-design-vue';
import { h } from 'vue';

export const columns: BasicColumn[] = [
  {
    title: '标识',
    dataIndex: 'name',
    width: 120,
  },
  {
    title: '名称',
    dataIndex: 'realName',
    width: 120,
    slots: { customRender: 'realName' },
  },
  {
    title: '状态',
    dataIndex: 'status',
    width: 80,
    customRender: ({ record }) => {
      const enable = record.status === Status.Valid;
      const color = enable ? 'green' : 'red';
      const text = enable ? '启用' : '停用';
      return h(Tag, { color: color }, () => text);
    },
  },
  {
    title: '备注',
    dataIndex: 'remark',
    width: 120,
  },
];

export const searchFormSchema: FormSchema[] = [
  {
    field: 'name',
    label: '标识',
    component: 'Input',
    colProps: { span: 6 },
  },
  {
    field: 'realName',
    label: '名称',
    component: 'Input',
    colProps: { span: 6 },
  },
];

export const roleSchemas: FormSchema[] = [
  {
    field: 'name',
    component: 'Input',
    label: '角色标识',
    helpMessage: '英文字符和数字组合,不允许超过50个字符',
    rules: [
      {
        required: true,
        message: '角色标示不允许为空',
      },
      {
        max: 50,
        message: '角色标示长度不允许超过50个字符',
        validateTrigger: ['change', 'blur'],
      },
      {
        type: 'string',
        pattern: new RegExp('^\\w+$'),
        message: '角色格式不正确',
        validateTrigger: ['change', 'blur'],
      },
    ],
  },
  {
    field: 'realName',
    component: 'Input',
    label: '角色名称',
    rules: [
      {
        required: true,
        message: '角色名称不允许为空',
      },
      {
        max: 50,
        message: '角色名称长度不允许超过50个字符',
        validateTrigger: ['change', 'blur'],
      },
    ],
  },
  {
    field: 'sort',
    component: 'InputNumber',
    label: '排序',
  },
  {
    field: 'isDefault',
    label: '默认',
    component: 'RadioButtonGroup',
    componentProps: {
      options: [
        { label: '是', value: true },
        { label: '否', value: false },
      ],
    },
    defaultValue: false,
  },
  {
    field: 'isPublic',
    label: '公开',
    component: 'RadioButtonGroup',
    componentProps: {
      options: [
        { label: '是', value: true },
        { label: '否', value: false },
      ],
    },
    defaultValue: false,
  },
  {
    field: 'status',
    label: '状态',
    component: 'RadioButtonGroup',
    componentProps: {
      options: statusOptions,
    },
    defaultValue: Status.Valid,
  },
  {
    field: 'remark',
    component: 'InputTextArea',
    label: '备注',
  },
];