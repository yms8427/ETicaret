<template>
  <div>
    <div class="row">
      <div class="col-6">
        <div class="alert alert-success fade-in customHeight" v-if="isAddingSuccess">
          <button
            type="button"
            class="close"
            data-dismiss="alert"
            aria-label="Close"
            @click="isAddingSuccess = false"
          >
            <span aria-hidden="true">&times;</span>
          </button>
          <strong>İşlem Başarıyla Gerçekleşti.</strong> <br /><br /><b
            >Ürün Kodu: </b
          >{{ successId }}
        </div>
      </div>
      <div class="col-6">
        <div
          class="alert alert-success fade-in customHeight"
          v-if="isUploadingSuccess"
        >
          <button
            type="button"
            class="close"
            data-dismiss="alert"
            aria-label="Close"
            @click="isUploadingSuccess = false"
          >
            <span aria-hidden="true">&times;</span>
          </button>
          <strong>Yükleme Tamamlandı.</strong> <br /><br />
        </div>
      </div>
    </div>
    <div class="row">
      <div class="card col-md-6">
        <div class="card-header"><strong>Ürün Ekle</strong></div>
        <div class="card-body">
          <div class="row">
            <div class="col-md-12">
              <form
                class="form-horizontal"
                action=""
                method="post"
                enctype="multipart/form-data"
              >
                <div class="form-group row">
                  <label class="col-md-3 col-form-label" for="name"
                    >Ürün Adı</label
                  >
                  <div class="col-md-9">
                    <input
                      class="form-control"
                      id="name"
                      type="text"
                      name="text-input"
                      placeholder="Ürün adını giriniz..."
                      v-model="NewProduct.name"
                      autocomplete="off"
                    />
                  </div>
                </div>
                <div class="form-group row">
                  <label class="col-md-3 col-form-label" for="price"
                    >Fiyatı</label
                  >
                  <div class="controls col-md-9">
                    <div class="input-group">
                      <input
                        class="form-control"
                        id="price"
                        type="text"
                        placeholder="Ürün fiyatını giriniz..."
                        v-model="NewProduct.price"
                        autocomplete="off"
                      />
                      <div class="input-group-append">
                        <span class="input-group-text">₺</span>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="form-group row">
                  <label class="col-md-3 col-form-label" for="stock"
                    >Stok Miktarı</label
                  >
                  <div class="col-md-9">
                    <input
                      class="form-control"
                      id="stock"
                      type="text"
                      name="text-input"
                      placeholder="Stok miktarını giriniz..."
                      v-model="NewProduct.stock"
                      autocomplete="off"
                    />
                  </div>
                </div>

                <div class="form-group row">
                  <label class="col-md-3 col-form-label" for="Catgory"
                    >Kategori</label
                  >
                  <div class="col-md-9">
                    <select
                      @change="CategoryChanged"
                      class="form-control"
                      id="Catgory"
                      name="select1"
                    >
                      <option :selected="defaultCategory" value="">
                        Kategoriyi Seçiniz
                      </option>
                      <option :value="c.id" v-for="c in Categories" :key="c.id">
                        {{ c.name }}
                      </option>
                    </select>
                  </div>
                </div>

                <div class="form-group row">
                  <label class="col-md-3 col-form-label" for="SubCatgory"
                    >Alt Kategori</label
                  >
                  <div class="col-md-9">
                    <select
                      v-model="NewProduct.subcategoryId"
                      class="form-control"
                      id="SubCatgory"
                      name="select1"
                    >
                      <option value="">Alt kategoriyi Seçiniz</option>
                      <option
                        v-for="s in SubCategories"
                        :value="s.id"
                        :key="s.id"
                      >
                        {{ s.name }}
                      </option>
                    </select>
                  </div>
                </div>

                <div class="form-group row">
                  <label class="col-md-3 col-form-label" for="Supplier"
                    >Tedarikçi</label
                  >
                  <div class="col-md-9">
                    <select
                      v-model="NewProduct.supplierId"
                      class="form-control"
                      id="Supplier"
                      name="select1"
                    >
                      <option value="">Tedarikçiyi Seçiniz</option>
                      <option v-for="s in Suppliers" :value="s.id" :key="s.id">
                        {{ s.name }}
                      </option>
                    </select>
                  </div>
                </div>
              </form>
            </div>
          </div>
        </div>

        <div class="card-footer">
          <button
            class="btn btn-sm btn-primary ml-1 w-25 float-right"
            type="submit"
            @click="AddNewProduct"
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
      <div v-if="successId != null" class="card col-md-6">
        <div class="card-header"><strong>Ürün Fotoğrafı Yükle</strong></div>
        <div class="card-body">
          <div class="row">
            <form
              class="form-horizontal"
              action=""
              method="post"
              enctype="multipart/form-data"
            >
              <div class="col-md-12">
                <div class="form-group row">
                  <div class="col-md-9">
                    <input
                      type="file"
                      ref="productPhoto"
                      v-on:change="handleFileUpload()"
                    />
                  </div>
                </div>
              </div>
            </form>
          </div>
        </div>

        <div class="card-footer">
          <button
            class="btn btn-sm btn-primary ml-1 w-25 float-right"
            type="submit"
            @click="upload"
          >
            Yükle
          </button>
          <button class="btn btn-sm btn-danger w-25 float-right" type="reset">
            Sil
          </button>
        </div>
      </div>

      <!-- /.col-->
    </div>
  </div>
</template>
<script>
import ajax from "../../helpers/ajax";
export default {
  name: "NewProduct",
  data() {
    return {
      Categories: [],
      SubCategories: [],
      Suppliers: [],
      NewProduct: {
        name: "",
        price: null,
        stock: null,
        subcategoryId: "",
        supplierId: "",
      },
      photo: null,

      isUploadingSuccess: false,
      isAddingSuccess: false,
      successId: null,
      defaultCategory: false,
    };
  },
  mounted() {
    var self = this;
    LoadCategories(self);
    LoadSuppliers(self);
  },
  methods: {
    CategoryChanged(e) {
      var self = this;
      LoadSubCategories(self, e.target.value);
    },
    AddNewProduct() {
      var data = {};
      Object.assign(data, this.NewProduct);
      data.price = Number(data.price);
      data.stock = Number(data.stock);

      ajax.post("api/production/home/add-new", data, (data) => {
        this.successId = data;
        this.isAddingSuccess = true;
      });
    },
    RefreshAll() {
      this.NewProduct.name = "";
      this.NewProduct.price = null;
      this.NewProduct.stock = null;
      this.NewProduct.subcategoryId = "";
      this.NewProduct.supplierId = "";
      this.successId = null;
      this.defaultCategory = true;
    },
    handleFileUpload() {
      this.photo = this.$refs.productPhoto.files[0];
    },
    upload() {
      var fd = new FormData();
      fd.append("productId", this.successId);
      fd.append("content", this.photo);
      ajax.postFile("api/production/home/upload", fd, (d) => {
        this.isUploadingSuccess = d;
      });
    },
  },
};
function LoadCategories(self) {
  ajax.get("api/production/category/list", (data) => {
    self.Categories = data;
  });
}
function LoadSubCategories(self, categoryId) {
  ajax.get(`api/production/subcategory/list/${categoryId}`, (data) => {
    self.SubCategories = data;
  });
}
function LoadSuppliers(self) {
  ajax.get("api/production/supplier/list", (data) => {
    self.Suppliers = data;
  });
}
</script>


<style scoped>
.customHeight{
  min-height: 100px !important;
}
</style>