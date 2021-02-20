<template>
  <div>
    <!-- modal -->
    <div
      class="modal fade"
      id="exampleModalCenter"
      tabindex="-1"
      role="dialog"
      aria-labelledby="exampleModalCenterTitle"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
        <div class="modal-content">
          <div class="modal-header bg-danger">
            <h5 class="modal-title" id="exampleModalLongTitle">
              <span class="mr-5 text-light">Sağlayıcı Detayı</span>
            </h5>
            <button
              type="button"
              class="close"
              data-dismiss="modal"
              aria-label="Close"
            >
              <span aria-hidden="true" class=" text-light">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <div class="form-group row">
              <label class="col-md-3 col-form-label" for="address"
                ><b>Adresi</b></label
              >
              <div class="col-md-9">
                <input
                  class="form-control"
                  id="address"
                  type="text"
                  name="text-input"
                  readonly
                  v-model="detailedSupplier.address"
                />
              </div>
            </div>
            <div class="form-group row">
              <label class="col-md-3 col-form-label" for="taxNumber"
                ><b>Vergi Numarası</b></label
              >
              <div class="col-md-9">
                <input
                  class="form-control"
                  id="taxNumber"
                  type="text"
                  name="text-input"
                  readonly
                  v-model="detailedSupplier.taxNumber"
                />
              </div>
            </div>
            <div class="form-group row">
              <label class="col-md-3 col-form-label" for="vote"
                ><b>Oy</b></label
              >
              <div class="col-md-9">
                <input
                  class="form-control"
                  id="vote"
                  type="text"
                  name="text-input"
                  readonly
                  v-model="detailedSupplier.vote"
                />
              </div>
            </div>
            <div class="form-group row">
              <label class="col-md-3 col-form-label" for="voteCount"
                ><b>Oy Sayısı</b></label
              >
              <div class="col-md-9">
                <input
                  class="form-control"
                  id="voteCount"
                  type="text"
                  name="text-input"
                  readonly
                  v-model="detailedSupplier.voteCount"
                />
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button
              type="button"
              class="btn btn-secondary"
              data-dismiss="modal"
            >
              Kapat
            </button>
            <button
              type="button"
              class="btn btn-danger"
              data-dismiss="modal"
              @click="remove(detailedSupplier.id)"
            >
              Sil
            </button>
          </div>
        </div>
      </div>
    </div>
    <!-- /modal -->
    <div class="alert alert-success fade-in" v-if="successId != null">
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
      <div class="col-md-12 col-lg-6">
        <div class="card">
          <div class="card-header">
            <i class="fa fa-align-justify"></i><b>Sağlayıcı Listesi</b>
          </div>
          <div class="card-body custom-height">
            <table class="table table-responsive-sm table-hover table-striped">
              <thead>
                <tr>
                  <th>Firma Adı</th>
                  <th>Email Adresi</th>
                  <th>Telefon Numarası</th>
                  <th>Detaylar</th>
                </tr>
              </thead>
              <tbody>
                <tr
                  class=" fade-in"
                  v-for="s in suppliers"
                  :key="s.id"
                  :class="s.id == suppliers[0].id ? tableSuccess : ''"
                >
                  <td>{{ s.name }}</td>
                  <td>{{ s.mail }}</td>
                  <td>{{ s.phone }}</td>
                  <td>
                    <button
                      @click="ShowDetail(s.id)"
                      class="btn btn-outline-danger"
                    >
                      Detay
                    </button>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import VuePhoneNumberInput from "vue-phone-number-input";
import "vue-phone-number-input/dist/vue-phone-number-input.css";
import ajax from "../../helpers/ajax";
export default {
  name: "Suppliers",
  components: {
    VuePhoneNumberInput,
  },
  data() {
    return {
      detailedSupplier: {
        id: "",
        address: "",
        taxNumber: "",
        vote: "",
        voteCount: "",
      },
      suppliers: [],
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
      tableSuccess: "",
    };
  },
  mounted() {
    this.LoadSuppliers();
  },
  methods: {
    LoadSuppliers() {
      ajax.get("api/production/supplier/list", (data) => {
        this.suppliers = data;
      });
    },
    AddNewSupplier() {
      var self = this;
      var data = {};
      Object.assign(data, this.NewSupplier);
      data.address = `${this.address.street}, ${this.address.postalCode} ${this.address.town}/${this.address.city}`;
      ajax.post("api/production/supplier/add-new", data, (data) => {
        self.successId = data;
        self.tableSuccess = "table-success";
        self.LoadSuppliers();
      });
    },
    openModal() {},
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
    ShowDetail(id) {
      ajax.get(`api/production/supplier/supplier-detail/${id}`, (data) => {
        this.detailedSupplier.address = data.address;
        this.detailedSupplier.taxNumber = data.taxNumber;
        this.detailedSupplier.vote = data.vote;
        this.detailedSupplier.voteCount = data.voteCount;
        this.detailedSupplier.id = data.id;

        window.$("#exampleModalCenter").modal("show");
      });
    },
    remove(id) {
      ajax.post(`api/production/supplier/remove/${id}`, null, d => {
        if (d) {
          this.LoadSuppliers();
        }
      });
    }
  },
};
</script>

<style scoped>
.custom-height {
  height: 496px !important;
  overflow: scroll;
}
</style>