namespace Pelo.v2.Web.Commons
{
    public class ApiUrl
    {
        public const string BASE_API_URL = "http://103.77.167.96:20001";
        //public const string BASE_API_URL = "http://localhost:49577";

        #region Account

        public const string LOG_ON = BASE_API_URL + "/api/account/logon";

        #endregion

        #region Role

        public const string ROLE_GET_ALL = BASE_API_URL + "/api/role/all";

        public const string ROLE_PAGING = BASE_API_URL + "/api/role?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        public const string ROLE_INSERT = BASE_API_URL + "/api/role";

        public const string ROLE_UPDATE = BASE_API_URL + "/api/role";

        public const string ROLE_DELETE = BASE_API_URL + "/api/role/{0}";

        public const string GET_ROLE_ID = BASE_API_URL + "/api/role/{0}";

        #endregion

        #region Branch

        public const string BRANCH_GET_ALL = BASE_API_URL + "/api/branch/all";

        public const string BRANCH_PAGING = BASE_API_URL + "/api/branch?name={0}&hotline={1}&provinceId={2}&districtId={3}&wardId={4}&column_order={5}&sort_dir={6}&page={7}&page_size={8}";

        public const string BRANCH_INSERT = BASE_API_URL + "/api/branch";

        public const string BRANCH_UPDATE = BASE_API_URL + "/api/branch";

        public const string BRANCH_DELETE = BASE_API_URL + "/api/branch/{0}";

        public const string GET_BRANCH_ID = BASE_API_URL + "/api/branch/{0}";

        #endregion

        #region Department

        public const string DEPARTMENT_GET_ALL = BASE_API_URL + "/api/department/all";

        public const string DEPARTMENT_DELETE = BASE_API_URL + "/api/department/{0}";

        public const string DEPARTMENT_GET_BY_ID = BASE_API_URL + "/api/department/{0}";

        public const string DEPARTMENT_UPDATE = BASE_API_URL + "/api/department";

        public const string DEPARTMENT_GET_BY_PAGING = BASE_API_URL + "/api/department?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        #endregion

        #region User

        public const string USER_GET_BY_PAGING = BASE_API_URL + "/api/user?code={0}&full_name={1}&phone_number={2}&branch_id={3}&department_id={4}&role_id={5}&status={6}&column_order={7}&sort_dir={8}&page={9}&page_size={10}";

        public const string USER_DELETE = BASE_API_URL + "/api/user/{0}";

        public const string USER_INSERT = BASE_API_URL + "/api/user";

        public const string USER_GET_BY_ID = BASE_API_URL + "/api/user/{0}";

        public const string USER_UPDATE = BASE_API_URL + "/api/user";

        public const string USER_GET_ALL = BASE_API_URL + "/api/user/all";

        public const string USER_IS_DEFAULT_CRM = BASE_API_URL + "/api/user/crm_default";

        public const string USER_IS_DEFAULT_INVOICE = BASE_API_URL + "/api/user/invoice_default";

        #endregion

        #region AppConfig

        public const string APP_CONFIG_GET_BY_PAGING = BASE_API_URL + "/api/app_config?name={0}&description={1}&column_order={2}&sort_dir={3}&page={4}&page_size={5}";

        public const string APP_CONFIG_DELETE = BASE_API_URL + "/api/app_config/{0}";

        public const string APP_CONFIG_INSERT = BASE_API_URL + "/api/app_config";

        public const string APP_CONFIG_GET_BY_ID = BASE_API_URL + "/api/app_config/{0}";

        public const string APP_CONFIG_UPDATE = BASE_API_URL + "/api/app_config";

        #endregion

        #region CustomerGroup

        public const string CUSTOMER_GROUP_GET_BY_PAGING = BASE_API_URL + "/api/customer_group?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        public const string CUSTOMER_GROUP_INSERT = BASE_API_URL + "/api/customer_group";

        public const string CUSTOMER_GROUP_GET_BY_ID = BASE_API_URL + "/api/customer_group/{0}";

        public const string CUSTOMER_GROUP_UPDATE = BASE_API_URL + "/api/customer_group";

        public const string CUSTOMER_GROUP_DELETE = BASE_API_URL + "/api/customer_group/{0}";

        public const string CUSTOMER_GROUP_GET_ALL = BASE_API_URL + "/api/customer_group/all";

        #endregion

        #region CustomerVip

        public const string CUSTOMER_VIP_GET_BY_PAGING = BASE_API_URL + "/api/customer_vip?column_order={0}&sort_dir={1}&page={2}&page_size={3}";

        public const string CUSTOMER_VIP_INSERT = BASE_API_URL + "/api/customer_vip";

        public const string CUSTOMER_VIP_GET_BY_ID = BASE_API_URL + "/api/customer_vip/{0}";

        public const string CUSTOMER_VIP_UPDATE = BASE_API_URL + "/api/customer_vip";

        public const string CUSTOMER_VIP_DELETE = BASE_API_URL + "/api/customer_vip/{0}";

        public const string CUSTOMER_VIP_GET_ALL = BASE_API_URL + "/api/customer_vip/all";

        #endregion

        #region Customer

        public const string CUSTOMER_GET_BY_PAGING = BASE_API_URL + "/api/customer?code={0}&name={1}&province_id={2}&district_id={3}&ward_id={4}&address={5}&phone={6}&email={7}&customer_group_id={8}&customer_vip_id={9}&column_order={10}&sort_dir={11}&page={12}&page_size={13}";

        public const string CUSTOMER_INSERT = BASE_API_URL + "/api/customer";

        public const string CUSTOMER_GET_BY_ID = BASE_API_URL + "/api/customer/{0}";

        public const string CUSTOMER_UPDATE = BASE_API_URL + "/api/customer";

        public const string CUSTOMER_DELETE = BASE_API_URL + "/api/customer/{0}";

        public const string CUSTOMER_GET_BY_PHONE = BASE_API_URL + "/api/customer/get_by_phone?phone={0}";

        public const string CUSTOMER_GET_DETAIL = BASE_API_URL + "/api/customer/detail/{0}";

        #endregion

        #region CustomerSource

        public const string CUSTOMER_SOURCE_GET_ALL = BASE_API_URL + "/api/customer_source/all";

        public const string CUSTOMER_SOURCE_DELETE = BASE_API_URL + "/api/customer_source/{0}";

        public const string CUSTOMER_SOURCE_GET_BY_ID = BASE_API_URL + "/api/customer_source/{0}";

        public const string CUSTOMER_SOURCE_UPDATE = BASE_API_URL + "/api/customer_source";

        public const string CUSTOMER_SOURCE_GET_BY_PAGING = BASE_API_URL + "/api/customer_source?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        #endregion

        #region CrmStatus

        public const string CRM_STATUS_GET_ALL = BASE_API_URL + "/api/crm_status/all";

        public const string CRM_STATUS_DELETE = BASE_API_URL + "/api/crm_status/{0}";

        public const string CRM_STATUS_GET_BY_ID = BASE_API_URL + "/api/crm_status/{0}";

        public const string CRM_STATUS_UPDATE = BASE_API_URL + "/api/crm_status";

        public const string CRM_STATUS_GET_BY_PAGING = BASE_API_URL + "/api/crm_status?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        #endregion

        #region CrmType

        public const string CRM_TYPE_GET_ALL = BASE_API_URL + "/api/crm_type/all";

        public const string CRM_TYPE_DELETE = BASE_API_URL + "/api/crm_type/{0}";

        public const string CRM_TYPE_GET_BY_ID = BASE_API_URL + "/api/crm_type/{0}";

        public const string CRM_TYPE_UPDATE = BASE_API_URL + "/api/crm_type";

        public const string CRM_TYPE_GET_BY_PAGING = BASE_API_URL + "/api/crm_type?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        #endregion

        #region CrmPriority

        public const string CRM_PRIORITY_GET_ALL = BASE_API_URL + "/api/crm_priority/all";

        public const string CRM_PRIORITY_DELETE = BASE_API_URL + "/api/crm_priority/{0}";

        public const string CRM_PRIORITY_GET_BY_ID = BASE_API_URL + "/api/crm_priority/{0}";

        public const string CRM_PRIORITY_UPDATE = BASE_API_URL + "/api/crm_priority";

        public const string CRM_PRIORITY_GET_BY_PAGING = BASE_API_URL + "/api/crm_priority?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        #endregion

        #region ProductGroup

        public const string PRODUCT_GROUP_GET_ALL = BASE_API_URL + "/api/product_group/all";

        public const string PRODUCT_GROUP_DELETE = BASE_API_URL + "/api/product_group/{0}";

        public const string PRODUCT_GROUP_GET_BY_ID = BASE_API_URL + "/api/product_group/{0}";

        public const string PRODUCT_GROUP_UPDATE = BASE_API_URL + "/api/product_group";

        public const string PRODUCT_GROUP_GET_BY_PAGING = BASE_API_URL + "/api/product_group?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        #endregion

        #region ProductStatus

        public const string PRODUCT_STATUS_GET_ALL = BASE_API_URL + "/api/product_status/all";

        public const string PRODUCT_STATUS_DELETE = BASE_API_URL + "/api/product_status/{0}";

        public const string PRODUCT_STATUS_GET_BY_ID = BASE_API_URL + "/api/product_status/{0}";

        public const string PRODUCT_STATUS_UPDATE = BASE_API_URL + "/api/product_status";

        public const string PRODUCT_STATUS_GET_BY_PAGING = BASE_API_URL + "/api/product_status?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        #endregion

        #region Crm

        public const string CRM_GET_BY_PAGING = BASE_API_URL + "/api/crm?code={0}&customer_code={1}&customer_name={2}&customer_phone={3}&customer_address={4}&province_id={5}&district_id={6}&ward_id={7}&customer_group_id={8}&customer_vip_id={9}&customer_source_id={10}&product_group_id={11}&crm_status_id={12}&crm_type_id={13}&crm_priority_id={14}&visit={15}&from_date={16}&to_date={17}&user_created_id={18}&date_created={19}&user_care_id={20}&need={21}&page={22}&page_size={23}";

        public const string CRM_GET_CRM_CUSTOMER_BY_PAGING = BASE_API_URL + "/api/crm/get_by_customer?customerId={0}&page={1}&pageSize={2}";

        public const string CRM_GET_INVOICE_CUSTOMER_BY_PAGING = BASE_API_URL + "/api/invoice/get_by_custome?customerId={0}&page={1}&pageSize={2}";

        public const string CRM_INSERT = BASE_API_URL + "/api/crm";

        public const string GET_CRM_ID = BASE_API_URL + "/api/crm/{0}";

        public const string CRM_UPDATE = BASE_API_URL + "/api/crm";

        public const string CRM_COMMENT_UPDATE = BASE_API_URL + "/api/crm/comment";

        #endregion

        #region Province / District / Ward

        public const string PROVINCE_GET_BY_PAGING = BASE_API_URL + "/api/province?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        public const string PROVINCE_INSERT = BASE_API_URL + "/api/province";

        public const string PROVINCE_UPDATE = BASE_API_URL + "/api/province";

        public const string PROVINCE_DELETE = BASE_API_URL + "/api/province/{0}";

        public const string PROVINCE_GET_BY_ID = BASE_API_URL + "/api/province/{0}";

        public const string PROVINCE_GET_ALL = BASE_API_URL + "/api/province/all";

        public const string DISTRICT_GET_ALL = BASE_API_URL + "/api/district/{0}/all";

        public const string DISTRICT_GET_BY_PAGING = BASE_API_URL + "/api/district?name={0}&province_id={1}&column_order={2}&sort_dir={3}&page={4}&page_size={5}";

        public const string DISTRICT_INSERT = BASE_API_URL + "/api/district";

        public const string DISTRICT_UPDATE = BASE_API_URL + "/api/district";

        public const string DISTRICT_DELETE = BASE_API_URL + "/api/district/{0}";

        public const string DISTRICT_GET_BY_ID = BASE_API_URL + "/api/district/{0}";

        public const string WARD_GET_ALL = BASE_API_URL + "/api/ward/{0}/all";

        public const string WARD_DELETE = BASE_API_URL + "/api/ward/{0}";

        public const string WARD_GET_BY_ID = BASE_API_URL + "/api/ward/{0}";

        public const string WARD_UPDATE = BASE_API_URL + "/api/ward";

        public const string WARD_GET_BY_PAGING = BASE_API_URL + "/api/ward?name={0}&district_id={1}&province_id={2}&column_order={3}&sort_dir={4}&page={5}&page_size={6}";

        #endregion

        #region InvoiceStatus

        public const string INVOICE_STATUS_GET_ALL = BASE_API_URL + "/api/invoice_status/all";

        public const string INVOICE_STATUS_DELETE = BASE_API_URL + "/api/invoice_status/{0}";

        public const string INVOICE_STATUS_GET_BY_ID = BASE_API_URL + "/api/invoice_status/{0}";

        public const string INVOICE_STATUS_UPDATE = BASE_API_URL + "/api/invoice_status";

        public const string INVOICE_STATUS_GET_BY_PAGING = BASE_API_URL + "/api/invoice_status?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        #endregion

        #region Invoice

        public const string INVOICE_GET_BY_PAGING = BASE_API_URL + "/api/invoice?customer_code={0}&customer_phone={1}&customer_name={2}&code={3}&branch_id={4}&invoice_status_id={5}&user_created_id={6}&user_sell_id={7}&user_delivery_id={8}&from_date={9}&to_date={10}&page={11}&page_size={12}";

        public const string INVOICE_DELETE = BASE_API_URL + "/api/invoice/{0}";

        public const string INVOICE_GET_BY_ID = BASE_API_URL + "/api/invoice/{0}";

        public const string INVOICE_UPDATE = BASE_API_URL + "/api/invoice";

        public const string INVOICE_GET_ALL = BASE_API_URL + "/api/invoice/all";

        #endregion

        #region Product

        public const string PRODUCT_GET_ALL = BASE_API_URL + "/api/product/all";

        public const string PRODUCT_GET_BY_PAGING = BASE_API_URL + "/api/product?name={0}&product_status_id={1}&product_group_id={2}&product_unit_id={3}manufacturer_id={4}&country_id={5}&description{6}&warranty_month={7}&column_order={8}&sort_dir={9}&page={10}&page_size={11}";

        public const string PRODUCT_DELETE = BASE_API_URL + "/api/product/{0}";

        public const string PRODUCT_GET_BY_ID = BASE_API_URL + "/api/product/{0}";

        public const string PRODUCT_UPDATE = BASE_API_URL + "/api/product";

        #endregion

        #region ProductUnit

        public const string PRODUCT_UNIT_GET_ALL = BASE_API_URL + "/api/product_unit/all";

        public const string PRODUCT_UNIT_DELETE = BASE_API_URL + "/api/product_unit/{0}";

        public const string PRODUCT_UNIT_GET_BY_ID = BASE_API_URL + "/api/product_unit/{0}";

        public const string PRODUCT_UNIT_UPDATE = BASE_API_URL + "/api/product_unit";

        public const string PRODUCT_UNIT_GET_BY_PAGING = BASE_API_URL + "/api/product_unit?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        #endregion

        #region PayMethod

        public const string PAY_METHOD_GET_ALL = BASE_API_URL + "/api/pay_method/all";

        public const string PAY_METHOD_GET_BY_PAGING = BASE_API_URL + "/api/pay_method?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        public const string PAY_METHOD_DELETE = BASE_API_URL + "/api/pay_method/{0}";

        public const string PAY_METHOD_GET_BY_ID = BASE_API_URL + "/api/pay_method/{0}";

        public const string PAY_METHOD_UPDATE = BASE_API_URL + "/api/pay_method";

        #endregion

        #region Manufacturer

        public const string MANUFACTURER_GET_ALL = BASE_API_URL + "/api/manufacturer/all";

        public const string MANUFACTURER_DELETE = BASE_API_URL + "/api/manufacturer/{0}";

        public const string MANUFACTURER_GET_BY_ID = BASE_API_URL + "/api/manufacturer/{0}";

        public const string MANUFACTURER_UPDATE = BASE_API_URL + "/api/manufacturer";

        public const string MANUFACTURER_GET_BY_PAGING = BASE_API_URL + "/api/manufacturer?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        #endregion

        #region Candidate Status
        public const string CANDIDATE_STATUS_GET_BY_PAGING = BASE_API_URL + "/api/candidate_status?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        public const string CANDIDATE_STATUS_GET_ALL = BASE_API_URL + "/api/candidate_status/all";

        public const string CANDIDATE_STATUS_DELETE = BASE_API_URL + "/api/candidate_status/{0}";

        public const string CANDIDATE_STATUS_GET_BY_ID = BASE_API_URL + "/api/candidate_status/{0}";

        public const string CANDIDATE_STATUS_UPDATE = BASE_API_URL + "/api/candidate_status";
        #endregion

        #region Candidate
        public const string CANDIDATE_GET_BY_PAGING = BASE_API_URL + "/api/candidate?name={0}&from_date={1}&to_date={2}&phone={3}&code={4}&candidate_statusId={5}&column_order={6}&sort_dir={7}&page={8}&page_size={9}";

        public const string CANDIDATE_GET_ALL = BASE_API_URL + "/api/candidate/all";

        public const string CANDIDATE_DELETE = BASE_API_URL + "/api/candidate/{0}";

        public const string CANDIDATE_GET_BY_ID = BASE_API_URL + "/api/candidate/{0}";

        public const string CANDIDATE_UPDATE = BASE_API_URL + "/api/candidate";
        #endregion

        #region Receipt Status
        public const string RECEIPT_STATUS_GET_BY_PAGING = BASE_API_URL + "/api/receipt_status?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        public const string RECEIPT_STATUS_GET_ALL = BASE_API_URL + "/api/receipt_status/all";

        public const string RECEIPT_STATUS_DELETE = BASE_API_URL + "/api/receipt_status/{0}";

        public const string RECEIPT_STATUS_GET_BY_ID = BASE_API_URL + "/api/receipt_status/{0}";

        public const string RECEIPT_STATUS_UPDATE = BASE_API_URL + "/api/receipt_status";
        #endregion

        #region Receipt Description
        public const string RECEIPT_DESCRIPTION_GET_ALL = BASE_API_URL + "/api/receipt_description/all";

        public const string RECEIPT_DESCRIPTION_GET_BY_PAGING = BASE_API_URL + "/api/receipt_description?name={0}&column_order={1}&sort_dir={2}page={3}&page_size={4}";

        public const string RECEIPT_DESCRIPTION_INSERT = BASE_API_URL + "/api/receipt_description";

        public const string RECEIPT_DESCRIPTION_UPDATE = BASE_API_URL + "/api/receipt_description";

        public const string RECEIPT_DESCRIPTION_DELETE = BASE_API_URL + "/api/receipt_description/{0}";

        public const string GET_RECEIPT_DESCRIPTION_ID = BASE_API_URL + "/api/receipt_description/{0}";
        #endregion

        #region Warranty Status
        public const string WARRANTY_STATUS_GET_BY_PAGING = BASE_API_URL + "/api/warranty_status?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        public const string WARRANTY_STATUS_GET_ALL = BASE_API_URL + "/api/warranty_status/all";

        public const string WARRANTY_STATUS_DELETE = BASE_API_URL + "/api/warranty_status/{0}";

        public const string WARRANTY_STATUS_GET_BY_ID = BASE_API_URL + "/api/warranty_status/{0}";

        public const string WARRANTY_STATUS_UPDATE = BASE_API_URL + "/api/warranty_status";
        #endregion

        #region Warranty Description
        public const string WARRANTY_DESCRIPTION_GET_ALL = BASE_API_URL + "/api/receipt_description/all";

        public const string WARRANTY_DESCRIPTION_GET_BY_PAGING = BASE_API_URL + "/api/receipt_description?name={0}&column_order={1}&sort_dir={2}page={3}&page_size={4}";

        public const string WARRANTY_DESCRIPTION_INSERT = BASE_API_URL + "/api/receipt_description";

        public const string WARRANTY_DESCRIPTION_UPDATE = BASE_API_URL + "/api/receipt_description";

        public const string WARRANTY_DESCRIPTION_DELETE = BASE_API_URL + "/api/receipt_description/{0}";

        public const string GET_WARRANTY_DESCRIPTION_ID = BASE_API_URL + "/api/receipt_description/{0}";
        #endregion

        #region Task Status
        public const string TASK_STATUS_GET_BY_PAGING = BASE_API_URL + "/api/task_status?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        public const string TASK_STATUS_GET_ALL = BASE_API_URL + "/api/task_status/all";

        public const string TASK_STATUS_DELETE = BASE_API_URL + "/api/task_status/{0}";

        public const string TASK_STATUS_GET_BY_ID = BASE_API_URL + "/api/task_status/{0}";

        public const string TASK_STATUS_UPDATE = BASE_API_URL + "/api/task_status";
        #endregion

        #region Task Priority

        public const string TASK_PRIORITY_GET_ALL = BASE_API_URL + "/api/task_priority/all";

        public const string TASK_PRIORITY_DELETE = BASE_API_URL + "/api/task_priority/{0}";

        public const string TASK_PRIORITY_GET_BY_ID = BASE_API_URL + "/api/task_priority/{0}";

        public const string TASK_PRIORITY_UPDATE = BASE_API_URL + "/api/task_priority";

        public const string TASK_PRIORITY_GET_BY_PAGING = BASE_API_URL + "/api/task_priority?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        #endregion

        #region Task Type

        public const string TASK_TYPE_GET_ALL = BASE_API_URL + "/api/task_type/all";

        public const string TASK_TYPE_DELETE = BASE_API_URL + "/api/task_type/{0}";

        public const string TASK_TYPE_GET_BY_ID = BASE_API_URL + "/api/task_type/{0}";

        public const string TASK_TYPE_UPDATE = BASE_API_URL + "/api/task_type";

        public const string TASK_TYPE_GET_BY_PAGING = BASE_API_URL + "/api/task_type?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        #endregion

        #region Task Loop

        public const string TASK_LOOP_GET_ALL = BASE_API_URL + "/api/task_loop/all";

        public const string TASK_LOOP_DELETE = BASE_API_URL + "/api/task_loop/{0}";

        public const string TASK_LOOP_GET_BY_ID = BASE_API_URL + "/api/task_loop/{0}";

        public const string TASK_LOOP_UPDATE = BASE_API_URL + "/api/task_loop";

        public const string TASK_LOOP_GET_BY_PAGING = BASE_API_URL + "/api/task_loop?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        #endregion

        #region Recruitment Status
        public const string RECRUITMENT_STATUS_GET_BY_PAGING = BASE_API_URL + "/api/recruiment_status?name={0}&column_order={1}&sort_dir={2}&page={3}&page_size={4}";

        public const string RECRUITMENT_STATUS_GET_ALL = BASE_API_URL + "/api/recruiment_status/all";

        public const string RECRUITMENT_STATUS_DELETE = BASE_API_URL + "/api/recruiment_status/{0}";

        public const string RECRUITMENT_STATUS_GET_BY_ID = BASE_API_URL + "/api/recruiment_status/{0}";

        public const string RECRUITMENT_STATUS_UPDATE = BASE_API_URL + "/api/recruiment_status";
        #endregion
    }
}
