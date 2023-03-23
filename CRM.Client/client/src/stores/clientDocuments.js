import { defineStore } from "pinia";
import { api } from "src/boot/axios";

export const useClientDocumentsStore = defineStore("clientDocuments", {
  state: () => ({
    documents: [],
    document: {
      id: null,
      name: '',
      contentType: ''
    },
  }),
  actions: {
    getClientDocuments(clientId) {
      api
        .get("/ClientDocument/getClientDocuments", { params: { clientId: clientId } })
        .then((response) => {
          this.documents = response.data;
        });
    },
    async addDocument() {
      return await api.post("/ClientDocument/add", this.documents);
    },
    async downloadDocument(documentId, documentName) {

        await api
          .get("/ClientDocument/download", {
            responseType: "blob",
            params: {
              documentId: documentId,
            },
          })
          .then((response) => {
            const blob = new Blob([response.data], {
              type: response.data.type,
            });
            const link = document.createElement("a");
            link.href = URL.createObjectURL(blob);
            link.download = documentName;
            link.click();
            URL.revokeObjectURL(link.href);
          });

    },
    async deleteDocument(documentId) {
      return await api.delete("ClientDocument/delete", {
        params: {
          documentId: documentId,
        },
      });
    },
  },
});
