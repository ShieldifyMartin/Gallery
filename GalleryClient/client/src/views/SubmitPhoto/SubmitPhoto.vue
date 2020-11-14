<template>
  <div class="submit-photo">
    <h1>Submit photo</h1>
    <p class="error">{{ state.error }}</p>
    <form method="post" @submit.prevent="handleSubmit">
      <label for="file" class="photo-upload-label">choose a picture</label>
      <input
        type="file"
        id="file"
        class="photo-upload"
        ref="picture"
        @change="handleFileUpload"
      />
      <input
        type="text"
        v-model="state.location"
        name="location"
        placeholder="Location"
      />
      <input
        type="text"
        v-model="state.description"
        name="description"
        placeholder="Description"
        required
      />
      <select v-model="state.categoryId" class="categories-dropdown">
        <option value="" selected>Select category</option>
        <option
          v-for="category in state.categories"
          :key="category.id"
          :value="category.id"
          >{{ category.title }}</option
        >
      </select>
      <input type="submit" value="Create" class="submit-btn" />
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import router from "../../router";
import store from "../../store";
import { postService } from "../../services";

export default defineComponent({
  setup() {
    const state = reactive({
      categories: [],
      picture: null,
      location: "",
      description: "",
      categoryId: "",
      error: "",
      maxSize: 15728640
    });

    watchEffect(async () => {
      const categories = await postService.getCategories();
      state.categories = categories;
    });

    function handleFileUpload(e) {
      let file = e.target.files[0];

      if (!e.target.files.length) return;

      if (e.target.files.length === 1) {
        if (file.size > state.maxSize) {
          state.error = "Too large picture!";
        } else {
          state.picture = file;
        }
      } else {
        state.error = "Only one photo is allowed!";
      }
    }

    async function handleSubmit() {
      const { picture, location, description, categoryId } = state;

      const isCorrect = validate();

      if (!isCorrect) {
        return;
      }

      var response = await postService.create(
        store.state.auth.state.token,
        picture,
        location,
        description,
        categoryId
      );

      if (response === 401) {
        router.push("/login");
      } else if (response > 400) {
        state.error = "Something went wrong!";
      } else {
        router.push("/");
      }
    }

    const validate = () => {
      if (state.picture === null || state.description.length === 0) {
        state.error = "Invalid data!";
        return false;
      }

      if (state.description.length > 2000) {
        state.error = "Max description length is 2000!";
        return false;
      }

      if (state.location.length > 40) {
        state.error = "Max location length is 40!";
        return false;
      }

      return true;
    };

    return {
      state,
      handleSubmit,
      handleFileUpload
    };
  }
});
</script>

<styles src="./index.scss"></styles>
