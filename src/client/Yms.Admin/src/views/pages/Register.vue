<template>
  <div class="d-flex align-items-center min-vh-100">
    <div>
      <CModal
        @update:show="GoUsers"
        title="YM'S Mağazacılık"
        color="success"
        :show.sync="successModal"
      >
        <span style="color: red">{{ newUser.displayName }}</span> Kullanıcısı
        Başarıyla Oluşturulmuştur...
      </CModal>
    </div>
    <CContainer fluid>
      <CRow class="justify-content-center">
        <CCol md="6">
          <CCard class="mx-4 mb-0">
            <CCardBody class="p-4">
              <CForm>
                <h2 class="mb-5">Yeni Bir Kullanıcı Ekle</h2>
                <CInput
                  placeholder="Adı Soyadı"
                  autocomplete="off"
                  v-model="newUser.displayName"
                >
                  <template #prepend-content
                    ><CIcon name="cil-user"
                  /></template>
                </CInput>
                <CInput
                  placeholder="Kullanıcı Adı"
                  autocomplete="off"
                  v-model="newUser.userName"
                >
                  <template #prepend-content
                    ><CIcon name="cil-user"
                  /></template>
                </CInput>
                <CInput
                  placeholder="Email"
                  autocomplete="email"
                  prepend="@"
                  v-model="newUser.mail"
                />
                <CInput
                  placeholder="Telefon Numarası"
                  autocomplete="off"
                  v-model="newUser.phone"
                >
                  <template #prepend-content
                    ><CIcon name="cil-phone"
                  /></template>
                </CInput>
                <CSelect
                  placeholder="Rol"
                  :options="[
                    { value: 'Admin', label: 'Admin' }, //TODO: bu kısmı Api tarafından getir.
                    { value: 'Client', label: 'Müşteri' },
                    { value: 'Supplier', label: 'Sağlayıcı' },
                  ]"
                  @change="selectChanged"
                >
                  <template #prepend-content
                    ><CIcon name="cil-user-follow"
                  /></template>
                </CSelect>
                <CInput
                  placeholder="Şifre"
                  type="password"
                  autocomplete="off"
                  v-model="newUser.password"
                >
                  <template #prepend-content
                    ><CIcon name="cil-lock-locked"
                  /></template>
                </CInput>
                <CInput
                  placeholder="Şifre (Tekrar)"
                  type="password"
                  autocomplete="off"
                  class="mb-4"
                >
                  <template #prepend-content
                    ><CIcon name="cil-lock-locked"
                  /></template>
                </CInput>
                <CButton @click="AddNewUser" color="success" block
                  >Hesabı Oluştur</CButton
                >
              </CForm>
            </CCardBody>
            <CCardFooter class="p-4"> </CCardFooter>
          </CCard>
        </CCol>
      </CRow>
    </CContainer>
  </div>
</template>

<script>
import ajax from "../../helpers/ajax";
export default {
  name: "Register",
  data() {
    return {
      successModal: false,

      newUser: {
        displayName: "",
        userName: "",
        mail: "",
        phone: "",
        type: "",
        password: "",
      },
    };
  },
  methods: {
    selectChanged(e) {
      this.newUser.type = e.target.value;
    },
    AddNewUser() {
      ajax.post("api/account/register", this.newUser, (data) => {
        if (data) {
          this.successModal = true;
        }
      });
    },
    GoUsers() {
      this.$router.push("/users");
    },
  },
};
</script>
