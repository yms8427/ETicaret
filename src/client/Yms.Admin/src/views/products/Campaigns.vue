<template>
  <div>
    <div class="card col-md-6">
      <div class="card-header"><strong>Mesaj Yolla</strong></div>
      <div class="card-body">
        <div class="row">
          <div class="col-md-12">
            <div class="form-group row">
              <label class="col-md-3 col-form-label" for="message">Mesaj</label>
              <div class="col-md-9">
                <textarea
                  class="form-control"
                  id="message"
                  name="message"
                  rows="6"
                  placeholder="Mesajınızı giriniz..."
                  style="resize: none"
                  maxlength="140"
                  v-model="message"
                ></textarea>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="card-footer">
        <button
          class="btn btn-sm btn-primary ml-1 w-25 float-right"
          @click="SendMessage"
        >
          Gönder
        </button>
      </div>
    </div>
  </div>
</template>
<script>
import hub from "../../helpers/hub";
export default {
  name: "Campaigns",
  data() {
    return {
      message: "",
    };
  },
  methods: {
    SendMessage() {
      if (hub.state === "Connected") {
        hub.invoke("CampaignMessage", this.message);
      }
    },
  },
};
</script>