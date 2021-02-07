<template>
  <CRow>
    <CCol col="12" xl="12">
      <div class="row">
        <div class="col-12">
          <button class="btn btn-outline-danger float-right mb-3" @click="AddNewUser">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="18"
              height="18"
              fill="currentColor"
              class="bi bi-person-plus"
              viewBox="0 0 16 16"
            >
              <path
                d="M6 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm2-3a2 2 0 1 1-4 0 2 2 0 0 1 4 0zm4 8c0 1-1 1-1 1H1s-1 0-1-1 1-4 6-4 6 3 6 4zm-1-.004c-.001-.246-.154-.986-.832-1.664C9.516 10.68 8.289 10 6 10c-2.29 0-3.516.68-4.168 1.332-.678.678-.83 1.418-.832 1.664h10z"
              />
              <path
                fill-rule="evenodd"
                d="M13.5 5a.5.5 0 0 1 .5.5V7h1.5a.5.5 0 0 1 0 1H14v1.5a.5.5 0 0 1-1 0V8h-1.5a.5.5 0 0 1 0-1H13V5.5a.5.5 0 0 1 .5-.5z"
              />
            </svg>
            <span class="ml-2">Yeni Kullanıcı Ekle</span>
          </button>
        </div>
      </div>
      <CCard>
        <CCardHeader> Yönetim Paneli Kullanıcıları </CCardHeader>
        <CCardBody>
          <CDataTable
            hover
            striped
            :items="items"
            :fields="fields"
            :items-per-page="5"
            :active-page="activePage"
            :pagination="{ doubleArrows: false, align: 'center' }"
            @page-change="pageChange"
          >
          </CDataTable>
        </CCardBody>
      </CCard>
    </CCol>
  </CRow>
</template>

<script>
import ajax from "../../helpers/ajax";
export default {
  name: "Users",
  data() {
    return {
      items: [],
      fields: [
        { key: "displayName", label: "Adı", _classes: "font-weight-bold" },
        { key: "phone", label: "Telefon Numarası" },
        { key: "mail", label: "Email Adresi" },
        { key: "type", label: "Rolü" },
      ],
      activePage: 1,
    };
  },
  mounted() {
    this.LoadUsers();
  },
  watch: {
    $route: {
      immediate: true,
      handler(route) {
        if (route.query && route.query.page) {
          this.activePage = Number(route.query.page);
        }
      },
    },
  },
  methods: {
    LoadUsers() {
      ajax.get("api/account/list", (data) => {
        this.items = data;
      });
    },
    pageChange(val) {
      this.$router.push({ query: { page: val } });
    },
    AddNewUser(){
      this.$router.push("/register");
    },
  },
};
</script>
