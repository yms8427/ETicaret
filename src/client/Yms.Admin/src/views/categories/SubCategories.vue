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
        >Alt Kategori Kodu: </b
      >{{ successId }}
    </div>
    <div class="row">
      <div class="card col-md-12">
        <div class="card-header"><strong>Alt Kategori Ekle</strong></div>
        <div class="card-body">
          <div class="row">
            <div class="col-md-6">
              <form
                class="form-horizontal"
                action=""
                method="post"
                enctype="multipart/form-data"
              >
                <div class="form-group row">
                  <label class="col-md-3 col-form-label" for="Catgory"
                    >Üst Kategorisi</label
                  >
                  <div class="col-md-9">
                    <select
                      @change="Changed"
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
                  <label
                    class="col-md-3 col-form-label custom-height"
                    for="name"
                    >Adı</label
                  >
                  <div class="col-md-9">
                    <input
                      class="form-control"
                      id="name"
                      type="text"
                      name="text-input"
                      placeholder="Alt Kategori adını giriniz..."
                      v-model="NewSubCategory.name"
                    />
                  </div>
                </div>
              </form>
            </div>
            <div class="col-md-6 border rounded"><h1>Image Upload</h1></div>
          </div>
        </div>
        <div class="card-footer col-md-6">
          <button
            class="btn btn-sm btn-primary ml-1 w-25 float-right"
            type="submit"
            @click="AddNewSubCategory"
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
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "SubCategories",
  data() {
    return {
      Categories: [],
      NewSubCategory: {
        name: "",
        categoryId: "",
      },
      successId: null,
      defaultCategory: false,
    };
  },
  mounted() {
    var self = this;
    LoadCategories(self);
  },
  methods: {
    Changed(e) {
      this.NewSubCategory.categoryId = e.target.value;
    },
    AddNewSubCategory() {
      var self = this;
      var data = {};
      Object.assign(data, this.NewSubCategory);
      axios
        .post("https://localhost:5001/api/production/subcategory/add-new", data)
        .then((response) => {
            self.successId = response.data;
        });
    },
    RefreshAll() {
        this.defaultCategory = true;
        this.successId = null;
        this.NewSubCategory.categoryId = "";
        this.NewSubCategory.name = "";

    },
  },
};
function LoadCategories(self) {
  axios
    .get("https://localhost:5001/api/production/category/list")
    .then((response) => {
      self.Categories = response.data;
    });
}
</script>

<style scoped>
.custom-height {
  height: 150px !important;
}
</style>