<template>
  <div>
    <div class="alert alert-success" v-if="successId != null">
      <button
        type="button"
        class="close"
        data-dismiss="alert"
        aria-label="Close"
        @click="successId = null"
      >
        <span aria-hidden="true">&times;</span>
      </button>
      <strong>İşlem Başarıyla Gerçekleşti.</strong> <br /><br /><b
        >Sağlayıcı Kodu: </b
      >{{ successId }}
    </div>
    <div class="row">
      <div class="col-md-12 col-lg-6">
        <div class="card">
          <div class="card-header"><strong>Sağlayıcı Ekle</strong></div>
          <div class="card-body">
            <form
              class="form-horizontal"
              action=""
              method="post"
              enctype="multipart/form-data"
            >
              <div class="form-group row">
                <label class="col-md-3 col-form-label" for="name"
                  >Firma Adı</label
                >
                <div class="col-md-9">
                  <input
                    class="form-control"
                    id="name"
                    type="text"
                    name="text-input"
                    placeholder="Firma adını giriniz..."
                    v-model="NewSupplier.name"
                    autocomplete="off"
                  />
                </div>
              </div>

              <div class="form-group row">
                <label class="col-md-3 col-form-label" for="email">Email</label>
                <div class="col-md-9">
                  <input
                    class="form-control"
                    id="email"
                    type="text"
                    name="text-input"
                    placeholder="Mail adresini giriniz..."
                    v-model="NewSupplier.mail"
                    autocomplete="off"
                  />
                </div>
              </div>
              <div class="form-group row">
                <label class="col-md-3 col-form-label" for="phone"
                  >Telefon</label
                >
                <div class="col-md-9">
                  <vue-phone-number-input v-model="NewSupplier.phone" />
                </div>
              </div>
              <div class="form-group row">
                <label class="col-md-3 col-form-label" for="taxNumber"
                  >Vergi Numarası</label
                >
                <div class="col-md-9">
                  <input
                    class="form-control"
                    id="taxNumber"
                    type="text"
                    name="text-input"
                    placeholder="Vergi numarasını giriniz..."
                    v-model="NewSupplier.taxNumber"
                    autocomplete="off"
                  />
                </div>
              </div>
              <div class="border p-3 rounded">
                <div class="form-group">
                  <label for="street">Sokak/Cadde</label>
                  <input
                    v-model="address.street"
                    class="form-control"
                    id="street"
                    type="text"
                    autocomplete="off"
                  />
                </div>

                <div class="row">
                  <div class="form-group col-sm-4">
                    <label for="city">İl</label>
                    <input
                      v-model="address.city"
                      class="form-control"
                      id="city"
                      type="text"
                      autocomplete="off"
                    />
                  </div>
                  <div class="form-group col-sm-4">
                    <label for="town">İlçe</label>
                    <input
                      v-model="address.town"
                      class="form-control"
                      id="town"
                      type="text"
                      autocomplete="off"
                    />
                  </div>
                  <div class="form-group col-sm-4">
                    <label for="postal-code">Posta Kodu</label>
                    <input
                      class="form-control"
                      id="postal-code"
                      type="text"
                      v-model="address.postalCode"
                      autocomplete="off"
                    />
                  </div>
                </div>
              </div>
            </form>
          </div>
          <div class="card-footer">
            <button
              class="btn btn-sm btn-primary ml-1 w-25 float-right"
              type="submit"
              @click="AddNewSupplier"
            >
              Ekle
            </button>
            <button
              @click="RefreshAll"
              class="btn btn-sm btn-danger w-25 float-right"
              type="reset"
            >
              Temizle
            </button>
          </div>
        </div>
      </div>
      <div class="col-md-12 col-lg-6"></div>
    </div>
  </div>
</template>

<script>
import VuePhoneNumberInput from 'vue-phone-number-input';
import axios from "axios";
import 'vue-phone-number-input/dist/vue-phone-number-input.css';
export default {
  name: "Suppliers",
  components: {
    VuePhoneNumberInput
  },
  data() {
    return {
      NewSupplier: {
        name: "",
        mail: "",
        phone: "",
        taxNumber: "",
        address: "",
      },
      address: {
        street: "",
        city: "",
        town: "",
        postalCode: "",
      },

      successId: null,
    };
  },
  methods: {
    AddNewSupplier() {
      var self = this;
      var data = {};
      Object.assign(data, this.NewSupplier);
      data.address = `${this.address.street}, ${this.address.postalCode} ${this.address.town}/${this.address.city}`;
      axios
        .post("https://localhost:5001/api/production/supplier/add-new", data)
        .then((response) => {
          self.successId = response.data;
        });
    },
    RefreshAll() {
      this.NewSupplier.name = "";
      this.NewSupplier.mail = "";
      this.NewSupplier.phone = "";
      this.NewSupplier.taxNumber = "";
      this.NewSupplier.address = "";

      this.address.street = "";
      this.address.city = "";
      this.address.town = "";
      this.address.postalCode = "";
      this.successId = null;
    },
  },
};
</script>

<style>
</style>