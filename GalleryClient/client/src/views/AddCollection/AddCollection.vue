<template>
  <div class="add-collection">
    <h1>Add Collection</h1>
    <form method="post" @submit.prevent="handleSubmit">
      <input
        type="text"
        v-model="state.name"
        name="name"
        placeholder="Collection Name"
        required
      />
      <input type="submit" value="Add" class="submit-btn" />
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive } from "vue";
import useAlert from "../../components/Alert/UseAlert";
import router from "../../router";
import store from "../../store";
import { collectionService } from "../../services/index";
import config from "@/config";

export default defineComponent({
  setup() {
    const state = reactive({
      name: "",
    });

    const handleSubmit = async () => {        
      const { name } = state;

      if (name.length < config.minCollectionNameLength || name.length >= config.maxCollectionNameLength) {
        useAlert(
          `Description length is not between ${config.minCollectionNameLength} and ${config.maxCollectionNameLength}!`,
          false
        );
        return;
      }

      var response = await collectionService.add(
        store.state.auth.state.token,
        name
      );

      if (response == 401) {
        router.push("/login");
      } else if (response >= 200 && response < 300) {
        useAlert("Successful!", true);
        router.push("/" + response);
      } else {
        useAlert("Something went wrong!", false);
      }
    };

    return {
      state,
      handleSubmit,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
