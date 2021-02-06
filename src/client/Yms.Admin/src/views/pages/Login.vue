<template>
  <div class="c-app flex-row align-items-center">
    <CContainer>
      <CRow class="justify-content-center">
        <CCol md="8">
          <CCardGroup>
            <CCard class="p-4">
              <CCardBody>
                <CForm>
                  <h1>Giriş</h1>
                  <p class="text-muted">YMS Portaline Giriş Yap</p>
                  <CInput placeholder="Username" autocomplete="off" v-model="username">
                    <template #prepend-content
                      ><CIcon name="cil-user"
                    /></template>
                  </CInput>
                  <CInput
                    placeholder="Password"
                    type="password"
                    autocomplete="off"
                    v-model="password"
                  >
                    <template #prepend-content
                      ><CIcon name="cil-lock-locked"
                    /></template>
                  </CInput>
                  <CRow>
                    <CCol col="6" class="text-left">
                      <CButton color="primary" class="px-4" @click="login"
                        >Giriş Yap</CButton
                      >
                    </CCol>
                    <CCol col="6" class="text-right">
                      <CButton color="link" class="px-0"
                        >Şifremi Unuttum</CButton
                      >
                    </CCol>
                  </CRow>
                </CForm>
              </CCardBody>
            </CCard>
          </CCardGroup>
        </CCol>
      </CRow>
    </CContainer>
  </div>
</template>

<script>
import ajax from "../../helpers/ajax";
import session from "../../helpers/session";
export default {
  name: 'Login',
  data : () => ({
    username: "",
    password: ""
  }),
  methods: {
    login() {
      ajax.post("api/account/login", { username: this.username, password: this.password}, (data) => {
        session.authenticate(data);
        this.$router.push("/");
      })
    }
  }
}
</script>
