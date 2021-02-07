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
        >Kategori Kodu: </b
      >{{ successId }}
    </div>
    <div class="row">
      <div class="col-md-12 col-lg-6">
        <div class="card">
          <div class="card-header"><strong>Kategori Ekle</strong></div>
          <div class="card-body">
            <form
              class="form-horizontal"
              action=""
              method="post"
              enctype="multipart/form-data"
            >
              <div class="form-group row">
                <label class="col-md-3 col-form-label" for="name">Adı</label>
                <div class="col-md-9">
                  <input
                    class="form-control"
                    id="name"
                    type="text"
                    name="text-input"
                    placeholder="Kategori adını giriniz..."
                    v-model="NewCategory.name"
                  />
                </div>
              </div>
              <div class="form-group row">
                <label class="col-md-3 col-form-label" for="textarea-input"
                  >Açıklama</label
                >
                <div class="col-md-9">
                  <textarea
                    class="form-control"
                    id="textarea-input"
                    name="textarea-input"
                    rows="6"
                    placeholder="Kategori için açıklama metni yazınız..."
                    style="resize: none"
                    maxlength="140"
                    v-model="NewCategory.description"
                  ></textarea>
                </div>
              </div>
            </form>
          </div>
          <div class="card-footer">
            <button
              class="btn btn-sm btn-primary ml-1 w-25 float-right"
              type="submit"
              @click="AddNewCategory"
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
import ajax from "../../helpers/ajax";
export default {
  name: "Categories",
  data() {
    return {
      NewCategory: {
        name: "",
        description: "",
      },
      successId: null,
    };
  },
  methods: {
    AddNewCategory() {
      var self = this;
      var data = {};
      Object.assign(data, this.NewCategory);
      ajax.post("api/production/category/add-new", data, (data) => {
        self.successId = data;
      });
    },
    RefreshAll() {
      this.successId = null;
      this.NewCategory.name = "";
      this.NewCategory.description = "";
    },
  },
};
</script>

<style>
</style>